using System.Collections.Generic;
using UnityEngine;

public class Prison_Animation : MonoBehaviour
{
    public int Animation = 0;

    public Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger("Anim", Animation);
    }
}
