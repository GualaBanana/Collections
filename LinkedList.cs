namespace Collections;


public class LinkedList
{
    public class Node
    {
        int _value;
        Node? _next;

        public ref int Value => ref _value;
        public ref Node? Next => ref _next;
        public Node DeepCopy => new(_value, _next);

        public Node(int value, Node? next = null)
        {
            _value = value;
            _next = next;
        }
    }
}