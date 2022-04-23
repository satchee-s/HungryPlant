using UnityEngine;
using UnityEditor;

public class FindNeighbourNodes : EditorWindow
{
    GameObject nodeObject;
    Node node, neighbourNode;
    float searchDistance;
    LayerMask nodeLayer, level;
    RaycastHit hit;
    Ray ray;
    bool excludeLayer;

    [MenuItem("Tools/Find Neighbours...")]

    public static void ShowWindow()
    {
        GetWindow<FindNeighbourNodes>("Find Neighbours");
    }
    private void OnGUI()
    {
        nodeObject = EditorGUILayout.ObjectField(nodeObject, typeof(GameObject), true) as GameObject;
        level = EditorGUILayout.LayerField("Layer to exclude", level);
        searchDistance = EditorGUILayout.FloatField("Search distance ", searchDistance);
        excludeLayer = EditorGUILayout.Toggle("Exclude layer", excludeLayer);
        if (GUILayout.Button("Find"))
        {
            Neighbours();
        }
    }

    void Neighbours()
    {
        node = nodeObject.GetComponent<Node>();
        nodeLayer = LayerMask.GetMask("Nodes");
        //level = LayerMask.GetMask("Level");
        Collider[] col = Physics.OverlapSphere(node.Position, searchDistance, nodeLayer);
        foreach (Collider nodes in col)
        {
            neighbourNode = nodes.GetComponent<Node>();
            ray = new Ray(node.Position, nodes.transform.position);
            if ((excludeLayer?(!Physics.Raycast(ray, out hit, ~level)): !Physics.Raycast(ray, out hit)) && neighbourNode != node)
            //if (!Physics.Raycast(ray, out hit) && neighbourNode != node)
            {
                //Debug.DrawLine(node.Position, node.transform.position, Color.red);
                Debug.Log("Node found" + nodes.name);
                node.neighbours.Add(neighbourNode);
                //Debug.Log("Obstacle found" + hit.collider.name);
            }
        }
    }
}
