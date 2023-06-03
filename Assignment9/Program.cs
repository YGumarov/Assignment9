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
