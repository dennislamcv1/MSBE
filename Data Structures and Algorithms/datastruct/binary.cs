// The current binary tree implementation is used in SwiftCollab’s system

public class Node
{
    public int Value;
    public Node Left, Right;
    public Node(int value)
    {
        Value = value;
        Left = Right = null;
    }
}
        PrintInOrder(node.Left);
        Console.Write(node.Value + " ");
        PrintInOrder(node.Right);
    }
}

// 