using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadingScript : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeDuration;
    
    [SerializeField] private float instantFadeSpeed;

    [SerializeField] private bool fadeIn = false;
    

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cutsceneAudio;
    //[SerializeField] private string nextSceneName;
    
    
    
    private void Start()
    {
        if (fadeIn)
        {
            FadeIn();
        }
        else
        {
            FadeOut();
        }
    }
    
    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 0,fadeDuration));
    }
    
    public IEnumerator FadeOut()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1,fadeDuration));
    }
    
    public IEnumerator CutToBlack()
    {
        yield return StartCoroutine(FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1,instantFadeSpeed));
    }
    
    
    private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float duration)
    {
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            cg.alpha = Mathf.Lerp(start, end, elapsedTime / duration);
            yield return null;
        }
        cg.alpha = end;
    }
   

    public IEnumerator CutsceneSequence()
    {
        
        //StartCoroutine(
            //FadeCanvasGroup(canvasGroup, canvasGroup.alpha, 1, instantFadeSpeed)
        //);
        canvasGroup.alpha = 1f;

       
        audioSource.clip = cutsceneAudio;
        audioSource.Play();

       
        yield return new WaitForSeconds(cutsceneAudio.length);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
