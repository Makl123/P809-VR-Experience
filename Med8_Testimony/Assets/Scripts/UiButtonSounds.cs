using UnityEngine;

public class UiButtonSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    

    public void PlayClick(AudioClip clip)
    {
        Debug.Log("Button Pressed!");
        audioSource.PlayOneShot(clip);
    }
}
