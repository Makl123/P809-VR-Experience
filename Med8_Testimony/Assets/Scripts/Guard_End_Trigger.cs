using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Guard_End_Trigger : MonoBehaviour
{
    [SerializeField] private GameObject endTheGameText;
    
    [SerializeField] private InputActionReference primaryButtonAction;

    [SerializeField] private FadingScript fadingScript;
    [SerializeField] private SceneChanger sceneChanger;
    
    

    private bool playerInRange;
    private bool hasTriggered;

    void OnEnable()
    {
        primaryButtonAction.action.performed += OnPrimaryButtonPressed;
        primaryButtonAction.action.Enable();
    }

    void OnDisable()
    {
        primaryButtonAction.action.performed -= OnPrimaryButtonPressed;
        primaryButtonAction.action.Disable();
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            hasTriggered = false;
            endTheGameText.SetActive(true);
            //sceneChanger.LoadTheScene("End_Screen");
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playerInRange && !hasTriggered)
        {
            hasTriggered = true;
           
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            hasTriggered = false;
            endTheGameText.SetActive(false);
        }
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            sceneChanger.LoadTheScene("The_End");
            endTheGameText.SetActive(false);
            LSLMarkerSender.Instance.SendMarker("The_End");
        }
    }
    
}
