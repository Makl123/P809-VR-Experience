using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class VRButton : MonoBehaviour
{
    [SerializeField] private float deadTime;
    
    private bool deadTimeActive = false;
    
    public UnityEvent onPressed, onReleased;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Hand" && !deadTimeActive)
        {
            onPressed?.Invoke();
            Debug.Log("Pressed");
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
