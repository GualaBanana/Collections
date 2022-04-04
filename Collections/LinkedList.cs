namespace Collections;

public class LinkedList
{
    const int _minimalLength = 1;
    readonly LinkedListNode _head;
    int _length = -1;

    public LinkedListNode Head => _head;

    public LinkedListNode Tail => NodeAt(_minimalLength, 1)!;
    public int Length
    {
        get
        {
            if (_length == -1) _length = ToEnumerable().Count();
            return _length;
        }
    }

    public LinkedList(int size, Pattern pattern = Pattern.Fill, int patternSeed = 1)
        : this(size, pattern.GetEnumerable(patternSeed))
    {
    }

    public LinkedList(int size, IEnumerable<int> enumerable) : this(new())
    {
        if (size < _minimalLength) throw new ArgumentException($"Must be at least `_minimalLength = {_minimalLength}`.", nameof(size));

        var currentNode = Head;
        var nodeCount = _minimalLength;
        foreach (var value in enumerable)
        {
            currentNode.Value = value;
            if (nodeCount == size)
            {
                currentNode.Next = null;
                break;
            }
            currentNode = currentNode.Next = new LinkedListNode();
            nodeCount++;
        }
    }

    public LinkedList(LinkedListNode head)
    {
        _head = head;
    }

    public bool TryLink(LinkedListNode node, int to)
    {
        if (to < _minimalLength) throw new ArgumentException($"Must be at least `_minimalLength` = {_minimalLength}`.", nameof(to));

        var linkingNode = NodeAt(to);
        if (linkingNode is not null)
        {
            linkingNode.Next = node;
            return true;
        }
        return false;
    }

    public LinkedListNode? NodeAt(int position, int startPosition = 0) => startPosition switch
    {
        0 => NodeAtFromBeginning(position),
        1 => NodeAtFromEnd(position),
        _ => throw new ArgumentOutOfRangeException(nameof(startPosition), "Possible values are 0 (beginning) and 1 (end)."),
    };

    LinkedListNode? NodeAtFromBeginning(int position)
    {
        if (position < _minimalLength) throw new ArgumentException($"Must be at least `_minimalLength = {_minimalLength}`.", nameof(position));

        var currentPosition = 0;
        foreach (var node in ToEnumerable())
        {
            if (++currentPosition == position) return node;
        }
        return null;
    }

    LinkedListNode? NodeAtFromEnd(int position)
    {
        var lastNodes = new Queue<LinkedListNode>(position + 1);
        foreach (var node in ToEnumerable())
        {
            lastNodes.Enqueue(node);
            if (lastNodes.Count == position + 1) lastNodes.Dequeue();
        }
        return lastNodes.Dequeue();
    }

    public void Swap(int i, int j)
    {
        Swap(i, 0, j, 0);
    }

    public void Swap(int i, int iStartPosition, int j, int jStartPosition)
    {
        var a = NodeAt(i, iStartPosition);
        var b = NodeAt(j, jStartPosition);
        if (a is null || b is null) return;

        var aCopy = a.DeepCopy;
        var bCopy = b.DeepCopy;
        a.Value = bCopy.Value;
        b.Value = aCopy.Value;
    }

    public List<LinkedListNode> ToList()
    {
        return ToEnumerable().ToList();
    }

    public IEnumerable<LinkedListNode> ToEnumerable()
    {
        var currentNode = Head;
        do
        {
            yield return currentNode;
        }
        while ((currentNode = currentNode?.Next) is not null);
    }
}