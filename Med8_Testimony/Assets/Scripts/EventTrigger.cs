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
            LSLMarkerSender.Instance.SendMarker("Player Entered");
            Event = true;
        }

        if(other.CompareTag("AngiGuy") && AngiGuy == true)
        {
            LSLMarkerSender.Instance.SendMarker("AngiGuy Entered");
            Event = true;
        }

        if (other.CompareTag("Walker") && Walker == true)
        {
            LSLMarkerSender.Instance.SendMarker("Walker Entered");
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
