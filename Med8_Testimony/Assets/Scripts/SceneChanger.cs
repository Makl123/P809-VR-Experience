using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using TMPro;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private FadingScript _fadingScript;
    [SerializeField] private string sceneName;
    [SerializeField] private TextMeshPro goalScene;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        goalScene.text =  $"GET OVER HERE TO GO TO {sceneName}";
    }
    
    private void OnTriggerEnter(Collider other)
    {
        LoadTheScene(sceneName);
    }

    private void LoadScene(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    
    private void LoadTheScene(string nameOfScene)
    {
       
        StartCoroutine(StartGameCouroutine(nameOfScene));
    }
    
    public IEnumerator StartGameCouroutine(string nameOfScene)
    {
        yield return StartCoroutine(_fadingScript.CutToBlack());
        SceneManager.LoadScene(nameOfScene);
    }
    
   

    // Update is called once per frame
    void Update()
    {
        
    }
}
