namespace Collections;

public class LinkedListNode
{
    int _value;
    LinkedListNode? _next;

    public ref int Value => ref _value;
    public ref LinkedListNode? Next => ref _next;
    public LinkedListNode DeepCopy => new(_value, _next);

    public LinkedListNode() : this(default)
    {
    }

    public LinkedListNode(int value, LinkedListNode? next = null)
    {
        _value = value;
        _next = next;
    }
}
