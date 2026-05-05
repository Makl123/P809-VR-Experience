using UnityEngine;

public class NPCTriggerDialogue : MonoBehaviour
{
    public Dialogue dialogueSystem;

    private bool playerInRange;
    private bool hasTriggered;

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            hasTriggered = false;
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

    public void TryStartDialogue()
    {
        if (playerInRange && !hasTriggered)
        {
            hasTriggered = true;
            dialogueSystem.StartDialogue();
        }
    }
}
