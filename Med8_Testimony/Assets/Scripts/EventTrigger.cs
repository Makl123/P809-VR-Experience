using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public bool Event;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AngiGuy"))
        {
            Debug.Log("In");
            Event = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("AngiGuy"))
        {
            Debug.Log("In");
            Event = false;
        }
    }
}
