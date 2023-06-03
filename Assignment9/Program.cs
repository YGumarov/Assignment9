public class Edge<T>
{
    public T Source { get; }
    public T Destination { get; }
    public double Weight { get; }

    public Edge(T source, T destination, double weight)
    {
        Source = source;
        Destination = destination;
        Weight = weight;
    }
}

public class Vertex<T>
{
    public T Data { get; }
    public Dictionary<Vertex<T>, double> AdjacentVertices { get; }

    public Vertex(T data)
    {
        Data = data;
        AdjacentVertices = new Dictionary<Vertex<T>, double>();
    }

    public void AddAdjacentVertex(Vertex<T> destination, double weight)
    {
        AdjacentVertices[destination] = weight;
    }
}

public class WeightedGraph<T>
{
    public Dictionary<T, Vertex<T>> Vertices { get; }

    public WeightedGraph()
    {
        Vertices = new Dictionary<T, Vertex<T>>();
    }

    public void AddVertex(T data)
    {
        Vertices[data] = new Vertex<T>(data);
    }

    public void AddEdge(T source, T destination, double weight)
    {
        if (!Vertices.ContainsKey(source) || !Vertices.ContainsKey(destination))
        {
            throw new ArgumentException("Source or destination vertex not found.");
        }

        Vertices[source].AddAdjacentVertex(Vertices[destination], weight);
    }
}

public abstract class Search<T>
{
    protected WeightedGraph<T> Graph;

    protected Search(WeightedGraph<T> graph)
    {
        Graph = graph;
    }

    public abstract List<T> Execute(T start, T end);
}

public class BreadthFirstSearch<T> : Search<T>
{
    public BreadthFirstSearch(WeightedGraph<T> graph) : base(graph) { }

    public override List<T> Execute(T start, T end)
    {
        var visited = new HashSet<T>();
        var queue = new Queue<T>();
        var previous = new Dictionary<T, T>();

        visited.Add(start);
        queue.Enqueue(start);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            if (current.Equals(end))
            {
                return BuildPath(previous, start, end);
            }

            foreach (var neighbor in Graph.Vertices[current].AdjacentVertices.Keys)
            {
                if (!visited.Contains(neighbor.Data))
                {
                    visited.Add(neighbor.Data);
                    previous[neighbor.Data] = current;
                    queue.Enqueue(neighbor.Data);
                }
            }
        }

        return null;
    }

    private List<T> BuildPath(Dictionary<T, T> previous, T start, T end)
    {
        var path = new List<T> { end };

        while (!path[path.Count - 1].Equals(start))
        {
            path.Add(previous[path[path.Count - 1]]);
        }

        path.Reverse();
        return path;
    }
}