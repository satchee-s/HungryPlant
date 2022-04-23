using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Cutscene : MonoBehaviour
{
    protected GameObject player;
    public GameObject screenCanvas;
    public Camera playerCamera;
    public Camera cutScenecamera;
    protected CutsceneManager manager;
    protected float cutsceneDuration;
    float timer;

    private void Start()
    {
        manager = FindObjectOfType<CutsceneManager>();
        playerCamera = Camera.main;
    }
    public virtual void RunCutscene()
    {
        //run cutscene
        playerCamera.gameObject.SetActive(false);
        screenCanvas.SetActive(true);
        cutScenecamera.gameObject.SetActive(true);
        timer += Time.deltaTime;
        if (timer >= cutsceneDuration)
        {
            timer = 0;
            StopCutscene();
        }

    }
    public virtual void StopCutscene()
    {
        playerCamera.gameObject.SetActive(true);
        screenCanvas.SetActive(false);
        cutScenecamera.gameObject.SetActive(false);
    } //stop cutscene/reset all values
}
