using System.Collections;
using System.Collections.Generic;
using MyTree;

public static class TreeGenerator
{
    public static Tree GenerateTree(List<Node> nodes)
    {
        Tree tree = new Tree(nodes);
        return tree;
    }
}
