public class CircularLinkedListNode<T>{
    public CircularLinkedListNode(T value)
    {
        Value = value;
    }
    public T Value { get; set; }
    public CircularLinkedListNode<T> Next { get; set; }
    public CircularLinkedListNode<T> Previous { get; set; }
}