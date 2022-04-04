using FluentAssertions;
using System.Linq;
using Xunit;

namespace Collections.Test;

public class LinkedListTest
{
    [Fact]
    public void Constructor_Node_InitializesHead()
    {
        var node = new LinkedListNode(1);
        var linkedList = new LinkedList(node);

        linkedList.Head.Should().Be(node);
    }

    [Fact]
    public void Constructor_Size_CreatesDefaultLinkedList()
    {
        foreach (var size in Enumerable.Range(1, 100))
        {
            var linkedList = new LinkedList(size);
            var enumeratedLinkedList = linkedList.ToList();

            enumeratedLinkedList.Should().HaveCount(size);
            foreach (var node in enumeratedLinkedList.GetRange(0, size - 1))
            {
                node.Next.Should().NotBeNull();
            }
            enumeratedLinkedList[^1].Next.Should().BeNull();
        }
    }

    [Fact]
    public void Head_AlwaysPointsToHead()
    {
        var linkedList = new LinkedList(2);
        var controlHead = linkedList.Head;
        var head = linkedList.Head;

        head = head.Next;

        head.Should().NotBeSameAs(controlHead);
        linkedList.Head.Should().BeSameAs(controlHead);
    }

    [Fact]
    public void NodeAt_ValidPosition_ReturnsNode()
    {
        var position = 1;
        var linkedList = new LinkedList(position);

        linkedList.NodeAt(position).Should().NotBeNull();
    }

    [Fact]
    public void NodeAt_WrongPosition_ReturnsNull()
    {
        var position = 2;
        var linkedList = new LinkedList(position - 1);

        linkedList.NodeAt(position).Should().BeNull();
    }

    [Fact]
    public void NodeAt_FromEnd_ReturnsNode()
    {
        int size = 10;
        var linkedList = new LinkedList(size);
        LinkedListNode nodeFromEnd;
        LinkedListNode nodeFromBeginning;

        foreach (var position in Enumerable.Range(1, size))
        {
            nodeFromBeginning = linkedList.NodeAt(size + 1 - position)!;
            nodeFromEnd = linkedList.NodeAt(position, 1)!;

            nodeFromEnd.Should().BeSameAs(nodeFromBeginning);
        }
    }

    [Fact]
    public void Swap_SwapsNodesValues()
    {
        var linkedList = new LinkedList(2, Pattern.Ascending);
        var initialNode1 = linkedList.NodeAt(1)!.DeepCopy;
        var initialNode2 = linkedList.NodeAt(2)!.DeepCopy;

        linkedList.Swap(1, 2);
        var swappedNode1 = linkedList.NodeAt(1)!;
        var swappedNode2 = linkedList.NodeAt(2)!;

        swappedNode1.Value.Should().Be(initialNode2.Value);
        swappedNode2.Value.Should().Be(initialNode1.Value);
    }

    [Fact]
    public void TryLink_ToNodeAtValidIndex_LinksNode()
    {
        int size = 5;
        var node = new LinkedListNode();
        var linkedList = new LinkedList(size);
        var tail = linkedList.Tail;
        tail.Next.Should().BeNull();

        linkedList.TryLink(node, size);

        tail.Next.Should().BeSameAs(node);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(1000)]
    public void ToEnumerable_ReturnsEnumerable(int size)
    {
        var pattern = Pattern.Ascending;
        var linkedList = new LinkedList(size, pattern);

        int expected;
        int actual;
        foreach (var pair in pattern.GetEnumerable(1).Zip(linkedList.ToEnumerable().Select(node => node.Value)))
        {
            expected = pair.First;
            actual = pair.Second;
            actual.Should().Be(expected);
        }
    }
}
