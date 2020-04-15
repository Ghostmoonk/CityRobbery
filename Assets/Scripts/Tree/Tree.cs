using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTree
{
    public class Tree
    {
        public List<Node> nodes;
        int availableId = 0;

        public Tree(List<Node> newNodes)
        {
            nodes = new List<Node>();
            for (int i = 0; i < newNodes.Count; i++)
            {
                nodes.Add(newNodes[i]);
            }
        }

        public Node Insert(Node parentNode, int value)
        {
            Node node;
            if (parentNode == null)
            {
                node = new Node(availableId, value, -1, 0, 0);
            }
            else
            {
                node = new Node(availableId, value, parentNode.id, parentNode.level + 1, FetchNodesByLevel(parentNode.level + 1).Count);
            }
            availableId++;
            return node;
        }

        public List<Node> FetchNodesByLevel(int level)
        {
            List<Node> levelNodes = new List<Node>();
            for (int i = 0; i < nodes.Count; i++)
            {
                if (nodes[i].level == level)
                {
                    levelNodes.Add(nodes[i]);
                }
            }
            Debug.Log("Dans le nieau : " + level + ", il y a " + levelNodes.Count + " nodes.");
            return levelNodes;
        }
    }

    [System.Serializable]
    public class Node
    {
        public int id;
        public int value;
        public int parentId;
        public int level;
        public int rank;

        public Node(int id, int value, int parentId, int level, int rank)
        {
            this.id = id;
            this.value = value;
            this.parentId = parentId;
            this.level = level;
            this.rank = rank;
        }
    }
}