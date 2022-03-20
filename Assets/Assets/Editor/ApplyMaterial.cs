using UnityEngine;
using UnityEditor;

public class ApplyMaterial : ScriptableWizard
{
    public Material material;

    [MenuItem("Tools/Apply Material...")]

    static void CreateWizard()
    {
        DisplayWizard("Apply Material", typeof(ApplyMaterial), "Apply");
    }

    void ObjectsSelected()
    {
        helpString = "";
        if (Selection.objects != null)
        {
            helpString = "Number of objects selected: " + Selection.objects.Length;
        }
    }

    void OnWizardCreate()
    {
        if (Selection.objects == null)
            return;
        foreach (GameObject obj in Selection.objects)
        {
            if (obj.GetComponent<Renderer>() != null)
            {
                obj.GetComponent<Renderer>().material = material;
            }
        }
    }
    private void OnEnable()
    {
        ObjectsSelected();
    }
    private void OnSelectionChange()
    {
        ObjectsSelected();
    }
}
