using UnityEngine;
using System.Collections;
using TMPro; 

public class Endscreen : MonoBehaviour
{
     [Header("Text Component")]
    public TextMeshPro dialogueText;

    [Header("Text Sequence")]
    [TextArea(2, 5)]
    public string[] messages;

    [Header("Per-Message Visible Durations")]
    [Tooltip("Set how long each message stays visible. " +
             "If there are fewer durations than messages, the last value will be used for the remaining messages.")]
    public float[] visibleDurations;
    
    [Header("Fade Settings")]
    public float fadeDuration = 1f;
    public float defaultVisibleDuration = 3f;

    [Header("Triggered Objects")]
    [Tooltip("0 = first message, 5 = sixth message, 6 = seventh message")]
    public int triggerOnMessageIndex = 5;

    public GameObject[] objectsToActivate;

    private void Start()
    {
        StartCoroutine(PlaySequence());
    }

    private IEnumerator PlaySequence()
    {
        
        SetAlpha(0f);

        for (int i = 0; i < messages.Length; i++)
        {
            
            dialogueText.text = messages[i];

            
            yield return StartCoroutine(FadeText(0f, 1f));

           
            if (i == triggerOnMessageIndex)
            {
                ActivateObjects();
            }

           
            yield return new WaitForSeconds(GetVisibleDuration(i));

            
            yield return StartCoroutine(FadeText(1f, 0f));
        }
    }

    private float GetVisibleDuration(int index)
    {
       
        if (visibleDurations == null || visibleDurations.Length == 0)
            return defaultVisibleDuration;

        
        if (index < visibleDurations.Length)
            return visibleDurations[index];

       
        return visibleDurations[visibleDurations.Length - 1];
    }

    private IEnumerator FadeText(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color color = dialogueText.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);

            color.a = alpha;
            dialogueText.color = color;

            yield return null;
        }

        
        color.a = endAlpha;
        dialogueText.color = color;
    }

    private void SetAlpha(float alpha)
    {
        Color color = dialogueText.color;
        color.a = alpha;
        dialogueText.color = color;
    }

    private void ActivateObjects()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            if (obj != null)
                obj.SetActive(true);
        }
    }
}