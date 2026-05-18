using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private FadingScript _fadingScript;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        StartCoroutine(StartGameCouroutine());

    }

    public IEnumerator StartGameCouroutine()
    {
        yield return StartCoroutine(_fadingScript.FadeOut());
        SceneManager.LoadScene("Maria-house scene");
        LSLMarkerSender.Instance.SendMarker("Start");
    }
}
