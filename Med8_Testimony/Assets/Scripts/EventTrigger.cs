using UnityEngine;

public class EventTrigger : MonoBehaviour
{
    public bool Event;
    public bool Player;
    public bool AngiGuy;
    public bool Walker;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && Player == true)
        {
            Event = true;
        }

        if(other.CompareTag("AngiGuy") && AngiGuy == true)
        {
            Event = true;
        }

        if (other.CompareTag("Walker") && Walker == true)
        {
            Event = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && Player == true)
        {
            Event = false;
        }

        if (other.CompareTag("AngiGuy") && AngiGuy == true)
        {
            Event = false;
        }

        if (other.CompareTag("Walker") && Walker == true)
        {
            Event = false;
        }
    }
}
