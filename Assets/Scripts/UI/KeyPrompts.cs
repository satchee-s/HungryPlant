using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyPrompts : MonoBehaviour
{
    public GameObject holder;
    Animator animator;
    Text prompt;

    bool prompted;
    bool scaleUp;
    bool scaleDown;
    string promptKey;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        prompt = GetComponent<Text>();

        holder.transform.localScale = Vector3.zero;
    }

    public void DisplayPrompt(string key)
    {
        promptKey = key;
        prompt.text = promptKey;
        prompted = true;
        scaleUp = true;
        scaleDown = false;
    }

    private void Update()
    {
        if (prompted)
        {
            if (Input.GetKeyDown(promptKey.ToLower()))
            {
                animator.SetTrigger("ScaleDown");
                scaleDown = true;
                scaleUp = false;
                prompted = false;
            }
        }

        if (holder.transform.localScale.x < 1 && scaleUp)
        {
            holder.transform.localScale = Vector3.Lerp(holder.transform.localScale, Vector3.one, 2 * Time.unscaledDeltaTime);
        }
        if (holder.transform.localScale.x > 0 && scaleDown)
        {
            holder.transform.localScale = Vector3.Lerp(holder.transform.localScale, Vector3.zero, 2 * Time.unscaledDeltaTime);
        }
    }
}
