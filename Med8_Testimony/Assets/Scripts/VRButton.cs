using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Feedback;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class VRButton : MonoBehaviour
{
    [Header("DeadTime")]
    [SerializeField] private float deadTime;
    
    [Header("Hands")]
    [SerializeField] private HapticImpulsePlayer leftHand;
    [SerializeField] private HapticImpulsePlayer rightHand;
    
    
    private bool deadTimeActive = false;
    
    public UnityEvent onPressed, onReleased;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hand")  && !deadTimeActive)
        {
            onPressed?.Invoke();
            SendHaptics(other);
            //Debug.Log("Pressed");
        }
    }

    private void SendHaptics(Collider other)
    {
        if (other.transform.IsChildOf(leftHand.transform))
        {
            leftHand.SendHapticImpulse(0.5f, 0.1f, 1f);
        }
        else if (other.transform.IsChildOf(rightHand.transform))
        {
            rightHand.SendHapticImpulse(0.5f, 0.1f, 1f);
        }
    }
    
    IEnumerator WaitForDeadTime()
    {
        deadTimeActive = true;
        yield return new WaitForSeconds(deadTime);
        deadTimeActive = false;
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
