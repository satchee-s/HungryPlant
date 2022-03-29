using UnityEngine;
using UnityEditor;

public class FindNeighbourNodes : EditorWindow
{
    GameObject nodeObject;
    Node node;
    float searchDistance;
    LayerMask nodeLayer;
    RaycastHit hit;
    Ray ray;

    [MenuItem("Tools/Find Neighbours...")]

    public static void ShowWindow()
    {
        GetWindow<FindNeighbourNodes>("Find Neighbours");
    }
    private void OnGUI()
    {
        nodeObject = EditorGUILayout.ObjectField(nodeObject, typeof(GameObject), true) as GameObject;
        searchDistance = EditorGUILayout.FloatField("Search distance ", searchDistance);
        if (GUILayout.Button("Find"))
        {
            Neighbours();
        }
    }

    void Neighbours()
    {
        node = nodeObject.GetComponent<Node>();
        nodeLayer = LayerMask.GetMask("Nodes");
        Collider[] col = Physics.OverlapSphere(node.Position, searchDistance, nodeLayer);
        foreach (Collider nodes in col)
        {
            ray = new Ray(node.Position, nodes.transform.position);
            if (!Physics.Raycast(ray, out hit) && nodes.GetComponent<Node>() != node)
            {
                //Debug.DrawRay(node.Position, nodes.transform.position, Color.red);
                //Gizmos.DrawLine(node.Position, nodes.transform.position);
                Debug.DrawLine(node.Position, node.transform.position, Color.red);
                Debug.Log("Node found" + nodes.name);
                //Debug.Log("Obstacle found" + hit.collider.name);
            }
        }
    }
}
