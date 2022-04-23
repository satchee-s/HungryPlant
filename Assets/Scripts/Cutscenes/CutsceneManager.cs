using UnityEngine.SceneManagement;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Cutscene[] cutscenes;
    public Cutscene current;

    [HideInInspector] bool startCutscene;

    private void Update()
    {
        if (startCutscene)
        {
            current.RunCutscene();
            startCutscene = false;
        }
    }
}
