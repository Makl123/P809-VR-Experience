using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CaseSolver : MonoBehaviour
{
    [SerializeField] string[] tags;
    [SerializeField] AudioSource YouDidIt;
    [SerializeField] GameObject Unsolved;
    [SerializeField] GameObject Solved;

    // Tracks which tags have been found
    readonly HashSet<string> foundTags = new HashSet<string>();

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;
        CheckAndRegisterTag(other.tag);
    }

    private bool CheckTag(RaycastHit hit)
    {
        if (hit.collider == null) return false;
        return CheckAndRegisterTag(hit.collider.tag);
    }

    // Centralized check + registration. Returns true if tag is one we're tracking.
    private bool CheckAndRegisterTag(string tag)
    {
        if (tags == null || tags.Length == 0) return false;
        if (string.IsNullOrEmpty(tag)) return false;

        // Only consider tags that are in the configured list
        for (int i = 0; i < tags.Length; i++)
        {
            if (tag == tags[i])
            {
                // If this tag was already found, still acknowledge but don't double-count
                if (foundTags.Add(tag))
                {
                    Debug.Log($"Found tag: {tag}");
                    if (YouDidIt != null) YouDidIt.Play();
                }
                else
                {
                    Debug.Log($"Tag already found: {tag}");
                }

                // If we've found them all, print a completion message
                if (foundTags.Count >= tags.Length)
                {
                    Debug.Log("Good job! All tags have been found.");
                    if (Solved != null) Solved.SetActive(true);
                    if (Unsolved != null) Unsolved.SetActive(false);
                }

                return true;
            }
        }

        return false;
    }

    // Optional: reset progress at runtime
    public void ResetFoundTags()
    {
        foundTags.Clear();
        Debug.Log("Tag progress reset.");
    }
}
