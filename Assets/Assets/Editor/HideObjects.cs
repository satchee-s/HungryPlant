using UnityEngine;
using UnityEditor;

public class HideObjects : EditorWindow
{
    string[] allTags;
    string chosenTag;
    GameObject[] objects;
    bool tagStatus;
    bool tagExists;
    [MenuItem("Tools/Hide Objects...")]
    public static void ShowWindow()
    {
        GetWindow<HideObjects>("Hide Objects");
    }

    private void OnGUI()
    {
#if UNITY_EDITOR
        chosenTag = EditorGUILayout.TextField("Enter tag", chosenTag);
        if (GUILayout.Button("Enable/Disable"))
        {
            Hide();
        }
#endif
    }
    void Hide()
    {
        allTags = UnityEditorInternal.InternalEditorUtility.tags;
        for (int i = 0; i < allTags.Length; i++)
        {
            if (allTags[i] == chosenTag)
            {
                objects = GameObject.FindGameObjectsWithTag(chosenTag);
                tagStatus = objects[0].GetComponent<MeshRenderer>().enabled;
                foreach (GameObject obj in objects)
                {
                    obj.GetComponent<MeshRenderer>().enabled = !tagStatus;
                }
                tagExists = true;
                break;
            }
        }
        if (!tagExists)
            Debug.Log("Tag Doesn't exist");
        tagExists = false;
    }
}
