using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FishController : MonoBehaviour
{
    [SerializeField] GameObject progressBars;
    [SerializeField] Transform topPivot;
    [SerializeField] Transform bottomPivot;
    [SerializeField] Transform fish;
    [SerializeField] float timeMultiplicator;
    [SerializeField] float smoothMotion;
    float fishSpeed;
    float fishDestination;
    float fishTimer;
    float fishPosition;

    [SerializeField] RectTransform hook;
    [SerializeField] float hookPullPower;
    [SerializeField] float hookGravity;
    [SerializeField] float progressBarIncrease;
    [SerializeField] float progressDegradation;
    float hookProgress;
    float hookPositon = 0f;
    float hookPullVelocity;
    float pivotDistance;
    float hookSize;

    [HideInInspector] public bool fishingActive = false;
    float maxFailTime = 3f;
    public int totalFishCaught;

    [SerializeField] Transform progressBar;

    private void Update()
    {
        if (fishingActive)
        {
            FishMovement();
            Hook();
            ProgressCheck();
        }
    }

    void ProgressCheck()
    {
        float min = hook.position.y - (hookSize / 2);
        float max = hook.position.y + (hookSize / 2);
        if (fish.position.y > min && fish.position.y < max)
        {
            hookProgress -= progressBarIncrease * Time.deltaTime;
            maxFailTime = 3f;
        }
        else
        {
            hookProgress += progressDegradation * Time.deltaTime;
        }
        Vector3 scale = progressBar.localScale;
        scale.y = hookProgress *-1;
        scale.y = Mathf.Clamp(scale.y, 0, 1);
        progressBar.localScale = scale;
        if (scale.y >= 1)
        {
            Win();
            Reset();
        }
        else if (scale.y <= 0)
        {
            maxFailTime -= Time.deltaTime;
            if (maxFailTime < 0)
            {
                Reset();
            }
        }
    }
    void Hook()
    {
        if (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space))
        {
            hookPullVelocity += (hookPullPower * Time.deltaTime);
            hookPullVelocity = Mathf.Abs(hookPullVelocity);
        }
        hookPullVelocity -= (hookGravity * Time.deltaTime);
        hookPositon += hookPullVelocity;
        hookPositon = hookPositon / pivotDistance * 100;
        if ((hookPositon < 0f || hookPositon >= 1f) && hookPullVelocity < 0f)
        {
            hookPullVelocity = 0f;
        }
        hook.position = Vector3.Lerp(bottomPivot.position, topPivot.position, hookPositon);
    }
    private void FishMovement()
    {
        fishTimer -= Time.deltaTime;
        if (fishTimer <= 0)
        {
            fishTimer = Random.value * timeMultiplicator;
            fishDestination = Random.value;
        }
        fishPosition = Mathf.SmoothDamp(fishPosition, fishDestination, ref fishSpeed, smoothMotion);
        fish.position = Vector3.Lerp(bottomPivot.position, topPivot.position, fishPosition);
    }

    void Win()
    {
        totalFishCaught++;
        Debug.Log("You won!");

    }
    void Reset()
    {
        maxFailTime = 3f;
        hookPullVelocity = 0f;
        hookProgress = 0f;
        progressBar.localScale = new Vector3(1, 0, 1);
        fishingActive = false;
        progressBars.SetActive(false);
    }

    public void StartGame()
    {
        progressBars.SetActive(true);
        fishingActive = true;
        pivotDistance = Vector3.Distance(topPivot.position, bottomPivot.position);
        hookPullPower = (hookPullPower / 100) * pivotDistance;
        hookGravity = (hookGravity / 100) * pivotDistance;
        hookPullPower = 8f;
        hookGravity = 2f;
        hookSize = hook.rect.height;
    }
}
