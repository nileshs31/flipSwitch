using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimationController : MonoBehaviour
{
    public Animator animator; // Reference to the Animator
    public string animationClipName; // Name of the animation clip
    public float[] keyframeTimes; // Predefined keyframe times (in seconds)
    private int currentKeyframeIndex = 0; // Index of the current keyframe
    public float triggerTime;
    private bool hasTriggeredFunction = false;
    public bool canSkipPages = true;
    private AnimationClip animationClip;
    public Button skipButton;
    void Start()
    {

        for(int i=0; i <keyframeTimes.Length; i++)
        {
            keyframeTimes[i] = keyframeTimes[i] / 60;
        }
        triggerTime /= 60;
        // Get the animation clip from the Animator
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name == animationClipName)
            {
                animationClip = clip;
                break;
            }
        }

        if (animationClip == null)
        {
            Debug.LogError("Animation clip not found in the Animator.");
        }
    }

    void Update()
    {
        if (animationClip != null)
        {
            // Check if the animation reaches the trigger time
            CheckTriggerTime();
        }
    }
    void CheckTriggerTime()
    {
        // Get the current normalized time
        float currentNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f;

        // Convert normalized time to seconds
        float currentTime = currentNormalizedTime * animationClip.length;


        if (currentTime >= keyframeTimes[keyframeTimes.Length - 1])
        {
            canSkipPages = false;
            Invoke("PerformTriggerAction", 0.5f);
        }

    }

    void PerformTriggerAction()
    {
        Debug.Log("Trigger Action executed");
        skipButton.onClick.Invoke();
    }


    public void MoveToNextKeyframe()
    {
        if (keyframeTimes.Length > 0 && canSkipPages == true)
        {
            // Get the current normalized time
            float currentNormalizedTime = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1.0f;

            // Convert current normalized time to seconds
            float currentTime = currentNormalizedTime * animationClip.length;

            // Find the next keyframe time greater than the current time
            foreach (float keyframeTime in keyframeTimes)
            {
                if (keyframeTime > currentTime)
                {
                    // Calculate normalized time for the next keyframe
                    float normalizedTime = keyframeTime / animationClip.length;

                    // Play the animation from the specified normalized time
                    animator.Play(animationClipName, 0, normalizedTime);
                    return; // Exit after moving to the next keyframe
                }
            }

            // If no greater keyframe is found, optionally loop back to the start
            animator.Play(animationClipName, 0, keyframeTimes[0] / animationClip.length);
        }
        else if (canSkipPages == false)
            PerformTriggerAction();
    }
}
