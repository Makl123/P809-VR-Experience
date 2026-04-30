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
    public int Animation = 0;

    // Changing texture of the Npc
    public Renderer childRenderer;
    public string childObjectName;

    public Texture defaultTexture;
    public Texture watchedTexture;

    // Track state so we only update texture when `watched` changes.
    private bool lastWatchedState;

    void Start()
    {
        animator = GetComponent<Animator>();
        // Ensure texture state matches initial `watched` value.
        lastWatchedState = !watched; // force UpdateTexture call on first
    }

    void Update()
    {

        if (isWalking == true)
        {
            MoveNpc();
        }

        if (isWalking == false)
        {
        }

        animator.SetInteger("Anim", Animation);

        // Update texture only when watched changes.
        if (watched != lastWatchedState)
        {
            UpdateChildTexture();
            lastWatchedState = watched;
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

    private void ResolveChildRenderer()
    {
        if (childRenderer != null) return;

        if (!string.IsNullOrEmpty(childObjectName))
        {
            Transform child = transform.Find(childObjectName);
            if (child != null)
            {
                childRenderer = child.GetComponent<Renderer>();
            }
        }

        if (childRenderer == null)
        {
            // fallback: first Renderer in children (including self)
            childRenderer = GetComponentInChildren<Renderer>();
        }

        if (childRenderer == null)
        {
            Debug.LogWarning($"{name}: No Renderer found on child for texture swapping. Assign one in the inspector or set childObjectName.");
        }
    }

    // Apply the correct texture to the child's material based on `watched`.
    private void UpdateChildTexture()
    {
        if (childRenderer == null)
        {
            ResolveChildRenderer();
            if (childRenderer == null) return;
        }

        // Accessing .material creates an instance at runtime which is usually desired
        // so the change affects only this instance and not the shared material.
        Material mat = childRenderer.material;
        if (watched)
        {
            if (watchedTexture != null)
            {
                mat.mainTexture = watchedTexture;
            }
        }
        else
        {
            if (defaultTexture != null)
            {
                mat.mainTexture = defaultTexture;
            }
        }
    }
}
