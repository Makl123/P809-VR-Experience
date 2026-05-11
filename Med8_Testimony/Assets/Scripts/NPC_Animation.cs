using UnityEngine;

public class NPC_Animation : MonoBehaviour
{
    // Variables for looking at the player
    public GameObject player;
    public bool watched;
    public bool disableLookAtMode = false;
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
    public AudioClip[] audioClips;
    [SerializeField] private AudioSource audioSource;
    
    //Randomizes the sounds
    private int[] shuffledIndices;
    private int currentShuffleIndex = 0;
    private int lastPlayedIndex = -1;

    // Track state so we only update texture when `watched` changes.
    private bool lastWatchedState;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        
        // If no AudioSource exists, add one
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
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
            PlayRandomAudioClip();
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
    private void InitializeShuffleBag()
    {
        if (audioClips == null || audioClips.Length == 0)
            return;

        shuffledIndices = new int[audioClips.Length];

        for (int i = 0; i < audioClips.Length; i++)
        {
            shuffledIndices[i] = i;
        }

        ShuffleIndices();

        currentShuffleIndex = 0;
    }

    private void ShuffleIndices()
    {
        
        for (int i = shuffledIndices.Length - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);

            (shuffledIndices[i], shuffledIndices[j]) = (shuffledIndices[j], shuffledIndices[i]);
        }

       
        if (shuffledIndices.Length > 1 &&
            lastPlayedIndex != -1 &&
            shuffledIndices[0] == lastPlayedIndex)
        {
            (shuffledIndices[0], shuffledIndices[1]) = (shuffledIndices[1], shuffledIndices[0]);
        }
    }
    
    private void PlayRandomAudioClip()
    {
        if (audioClips == null || audioClips.Length == 0 || audioSource == null)
            return;

        
        if (audioClips.Length == 1)
        {
            audioSource.clip = audioClips[0];
            audioSource.Play();
            lastPlayedIndex = 0;
            return;
        }

        
        if (shuffledIndices == null || shuffledIndices.Length != audioClips.Length)
        {
            InitializeShuffleBag();
        }

       
        if (currentShuffleIndex >= shuffledIndices.Length)
        {
            ShuffleIndices();
            currentShuffleIndex = 0;
        }

      
        int clipIndex = shuffledIndices[currentShuffleIndex];
        currentShuffleIndex++;

        
        audioSource.clip = audioClips[clipIndex];
        audioSource.Play();

      
        lastPlayedIndex = clipIndex;
    }

    void LookAtPlayer()
    {
        
        if (disableLookAtMode)
            return;

        if (watched && headToRotate != null && player != null)
        {
            headToRotate.LookAt(player.transform);

            Vector3 e = headToRotate.localEulerAngles;

            if (e.y > 180)
                e.y -= 360;

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
