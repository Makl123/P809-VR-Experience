using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.AdaptivePerformance;

public class CarSceneChanger : MonoBehaviour
{
    [SerializeField] private FadingScript _fadingScript;
    //[SerializeField] private AudioSource audioSource;
    //[SerializeField] private AudioClip cutsceneAudio;
    [SerializeField] private string sceneName;

    private void OnTriggerEnter(Collider other)
    {
        
        if (!other.CompareTag("Car"))return;
        Debug.Log("Treehit");
        LoadTheCinematicScene(sceneName);
        
        
        
       
    }
    
    private void LoadTheCinematicScene(string nameOfScene)
    {
       
        StartCoroutine(StartCinematicGameCouroutine(nameOfScene));
    }
    
    public IEnumerator StartCinematicGameCouroutine(string nameOfScene)
    {
        yield return StartCoroutine(_fadingScript.CutsceneSequence());
        SceneManager.LoadScene(nameOfScene);
        LSLMarkerSender.Instance.SendMarker($"Entered {nameOfScene}");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
