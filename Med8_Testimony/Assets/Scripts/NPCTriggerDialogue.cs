using UnityEngine;
using UnityEngine.InputSystem;

public class NPCTriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueSystem;
    [SerializeField] private InputActionReference primaryButtonAction;

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
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (playerInRange && !hasTriggered)
        {
            hasTriggered = true;
            dialogueSystem.StartDialogue();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            hasTriggered = false;
        }
    }

    private void OnPrimaryButtonPressed(InputAction.CallbackContext context)
    {
        if (playerInRange)
        {
            dialogueSystem.DialogueInput();
        }
    }
}
