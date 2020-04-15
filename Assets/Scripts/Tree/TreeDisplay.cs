using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MyTree;

public class TreeDisplay : MonoBehaviour
{
    [SerializeField] GameObject nodePrefab;
    [SerializeField] List<Node> nodes;
    MyTree.Tree tree;

    void Start()
    {
        tree = TreeGenerator.GenerateTree(nodes);
        DrawTree(tree);
    }

    private void OnValidate()
    {
        if (Application.isPlaying)
        {
            tree = TreeGenerator.GenerateTree(nodes);
            CleanDisplay();
            DrawTree(tree);
        }
    }

    public void DrawTree(MyTree.Tree tree)
    {
        Debug.Log("On va afficher : " + tree.nodes.Count + " nodes");
        for (int n = 0; n < tree.nodes.Count; n++)
        {
            Debug.Log(((float)(tree.nodes[n].rank + 1) / (float)tree.FetchNodesByLevel(tree.nodes[n].level).Count));
            float xPos = Mathf.Lerp(-20f * tree.FetchNodesByLevel(tree.nodes[n].level).Count, 20f * tree.FetchNodesByLevel(tree.nodes[n].level).Count, ((float)(tree.nodes[n].rank + 1) / (float)tree.FetchNodesByLevel(tree.nodes[n].level).Count));
            Debug.Log("Node " + n + " au rang et level :" + tree.nodes[n].rank + " " + tree.nodes[n].level + " :" + xPos);
            Vector3 nodePosition = new Vector3(
               xPos,
               0f - tree.nodes[n].level * 20,
               0f);
            GameObject nodeToInstantiate = Instantiate(nodePrefab, nodePosition, Quaternion.identity, GameObject.FindGameObjectWithTag("TreeCanvas").transform);
            nodeToInstantiate.GetComponentInChildren<Text>().text = tree.nodes[n].value.ToString();
        }
    }

    public void CleanDisplay()
    {
        foreach (Transform child in GameObject.FindGameObjectWithTag("TreeCanvas").transform)
        {
            Destroy(child.gameObject);
        }
    }

}
