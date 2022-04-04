using FluentAssertions;
using Xunit;

namespace Collections.Test;

public class NodeTest
{
    [Fact]
    public void Constructor_ValueAndNext_DoesProperInitialization()
    {
        var lastNode = new LinkedList.Node(2);
        var node = new LinkedList.Node(1, lastNode);

        lastNode.Value.Should().Be(2);
        lastNode.Next.Should().Be(null);
        node.Value.Should().Be(1);
        node.Next.Should().Be(lastNode);
    }

    [Fact]
    public void DeepCopy_DoesDeepCopy()
    {
        var originalNode = new LinkedList.Node(1);
        var deepCopyNode = originalNode.DeepCopy;

        deepCopyNode.Value = 2;
        deepCopyNode.Next = originalNode;

        originalNode.Value.Should().Be(1);
        originalNode.Next.Should().Be(null);
        deepCopyNode.Value.Should().Be(2);
        deepCopyNode.Next.Should().Be(originalNode);
    }

    [Fact]
    public void NodeProperties_CanBeChanged_ViaAnyReference()
    {
        var node = new LinkedList.Node(1);
        var nodeReference = node;

        nodeReference.Value = 2;
        nodeReference.Next = node;

        node.Value.Should().Be(2);
        node.Next.Should().Be(node);
        nodeReference.Value.Should().Be(2);
        nodeReference.Next.Should().Be(node);
    }
}
