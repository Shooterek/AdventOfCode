public class CircularLinkedList<T>{
    public CircularLinkedList()
    {
        Head = null;
    }
    public CircularLinkedListNode<T> Head { get; set; }

    public CircularLinkedListNode<T> Insert(T value){
        var node = new CircularLinkedListNode<T>(value);
        if(Head == null){
            node.Next = node;
            node.Previous = node;
            Head = node;
        }
        else{
            node.Next = Head;
            node.Previous = Head.Previous;
            Head.Previous.Next = node;
            Head.Previous = node;
        }

        return node;
    }
}