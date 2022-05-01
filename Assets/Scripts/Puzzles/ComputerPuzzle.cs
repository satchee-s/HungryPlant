using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerPuzzle : Puzzle
{
    [SerializeField] Canvas screenCanvas;
    [SerializeField] Canvas inventory;
    [SerializeField] PlayerMovement player;
    [SerializeField] FishController fishController;
    bool gameEnabled;
    [HideInInspector] public bool gameCompleted = false;
    float timer;
    [SerializeField] float requiredTime;
    void CastRod()
    {
        if (!fishController.fishingActive && fishController.totalFishCaught < 3)
        {
            if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
            {
                timer += Time.deltaTime;
                Debug.Log("Reeling in a fish...");
                if (timer >= requiredTime)
                {
                    Debug.Log("You caught a fish");
                    fishController.StartGame();
                    requiredTime = Random.Range(3, 8);
                    timer = 0;
                }
            }
            else
            {
                timer = 0;
            }
        }
        else if (fishController.totalFishCaught >= 3)
        {
            Debug.Log("You won the game");
            fishController.enabled = false;
            //subtitle.DisplaySubtitle("I finally beat that stupid game");
            //gameEnabled = false;
            gameCompleted = false;
            QuitGame();
        }
    }

    void QuitGame()
    {
        fishController.totalFishCaught = 0;
        screenCanvas.gameObject.SetActive(false);
        player.enabled = true;
        inventory.enabled = true;
        gameEnabled = false;
    }


    private void Update()
    {
        if (gameEnabled && !gameCompleted)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                QuitGame();
            }
            CastRod();
        }
        
    }
    public void StartGame()
    {
        gameEnabled = true;
        screenCanvas.gameObject.SetActive(true);
        player.enabled = false;
        inventory.enabled = false;
        requiredTime = Random.Range(3, 8);
    }
}
