using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneMenuManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private Transform buttonParent;
    [SerializeField] private FadingScript fadingScript;

    [Header("Layout")]
    [SerializeField] private int columns = 3;
    [SerializeField] private float spacing = 0.3f;

    private void Start()
    {
        GenerateSceneButtons();
    }

    private void GenerateSceneButtons()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        string currentScene = SceneManager.GetActiveScene().name;

        for (int i = 0; i < sceneCount; i++)
        {
            string path = SceneUtility.GetScenePathByBuildIndex(i);
            string sceneName = System.IO.Path.GetFileNameWithoutExtension(path);

            // Skip current scene (menu)
            if (sceneName == currentScene)
                continue;

            GameObject button = Instantiate(buttonPrefab, buttonParent);

            SetupButton(button, sceneName, i);
            PositionButton(button.transform, i);
        }
    }

    private void SetupButton(GameObject button, string sceneName, int index)
    {
        // Set label
        TextMeshPro text = button.GetComponentInChildren<TextMeshPro>();
        if (text != null)
            text.text = sceneName;

        // Hook event
        VRButton vrButton = button.GetComponent<VRButton>();

        if (vrButton == null)
        {
            Debug.LogWarning($"Button prefab missing VRButton: {button.name}");
            return;
        }

        vrButton.onPressed.AddListener(() => LoadScene(index));
    }

    private void PositionButton(Transform t, int i)
    {
        int row = i / columns;
        int col = i % columns;

        t.localPosition = new Vector3(
            col * spacing,
            -row * spacing,
            0f
        );
    }

    private void LoadScene(int index)
    {
        StartCoroutine(LoadSceneRoutine(index));
    }

    private IEnumerator LoadSceneRoutine(int index)
    {
        if (fadingScript != null)
            yield return StartCoroutine(fadingScript.FadeOut());

        SceneManager.LoadScene(index);
    }
}
