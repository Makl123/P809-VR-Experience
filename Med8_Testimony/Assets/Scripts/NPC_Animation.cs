using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    // Variables for looking at the player
    public GameObject player;
    public bool watched;
    public Transform headToRotate;
    public float minY = -60f;
    public float maxY = 60f;

    // For moving the Npc towards an object
    public Transform Point;
    public float moveSpeed = 3f;

    // For animating the Npcs
    public Animator animator;
    public bool isWalking;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (isWalking == true)
        {
            animator.SetBool("IsWalking", true);
            MoveNpc();
        }

        if (isWalking == false)
        {
            animator.SetBool("IsWalking", false);
        }  
    }

    // LateUpdate for overriding the animations transform on the head.
    private void LateUpdate()
    {
        LookAtPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("In");
            watched = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Out");
            watched = false;
        }
    }

    void LookAtPlayer()
    {
        if (watched == true)
        {
            headToRotate.LookAt(player.transform);

            Vector3 e = headToRotate.localEulerAngles;

            if (e.y > 180) e.y -= 360;

            e.y = Mathf.Clamp(e.y, minY, maxY);

            headToRotate.localEulerAngles = e;
        }
    }

    void MoveNpc()
    {
        transform.LookAt(Point.transform);
        transform.position = Vector3.MoveTowards(
    transform.position,
    Point.position,
    moveSpeed * Time.deltaTime);
    }
}
