using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    public Animator animator;
    public bool isWalking;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (isWalking == true)
        {
            animator.SetBool("IsWalking", true);
        }

        if (isWalking == false)
        {
            animator.SetBool("IsWalking", false);
        }  
    }
}
