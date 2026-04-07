using UnityEngine;

public class NpcController : MonoBehaviour
{
    public GameObject player;
    public bool watched;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
    }


    void LookAtPlayer()
    {
        if (watched == true)
        {
            transform.LookAt(player.transform);
        }
    }
}
