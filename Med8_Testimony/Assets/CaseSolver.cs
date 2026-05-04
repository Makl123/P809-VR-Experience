using UnityEngine;
using System.Collections.Generic;

public class CaseSolver : MonoBehaviour
{
    public enum CaseId { A = 0, B = 1, C = 2 }

    [SerializeField] string[] CaseA;
    [SerializeField] string[] CaseB;
    [SerializeField] string[] CaseC;
    [SerializeField] AudioSource YouDidIt;
    [SerializeField] GameObject Unsolved;
    [SerializeField] GameObject Solved;

    // Track placed tags per case (prevents double-counting same tag in a case)
    readonly HashSet<string> foundA = new HashSet<string>();
    readonly HashSet<string> foundB = new HashSet<string>();
    readonly HashSet<string> foundC = new HashSet<string>();

    // Public API for trigger forwarders to call.
    // caseId: which case the tag was placed into (A, B or C)
    public void RegisterTagForCase(string tag, CaseId caseId)
    {
        if (string.IsNullOrEmpty(tag)) return;

        bool newlyAdded = false;
        bool correct = false;

        switch (caseId)
        {
            case CaseId.A:
                newlyAdded = foundA.Add(tag);
                correct = IsTagInArray(tag, CaseA);
                break;
            case CaseId.B:
                newlyAdded = foundB.Add(tag);
                correct = IsTagInArray(tag, CaseB);
                break;
            case CaseId.C:
                newlyAdded = foundC.Add(tag);
                correct = IsTagInArray(tag, CaseC);
                break;
        }

        // Provide immediate feedback
        if (newlyAdded)
        {
            Debug.Log($"Case {caseId}: placed tag '{tag}' -> {(correct ? "Correct" : "Incorrect")}");
            if (correct && YouDidIt != null) YouDidIt.Play();
        }
        else
        {
            Debug.Log($"Case {caseId}: tag '{tag}' already placed (ignored).");
        }

        // Auto-evaluate when the number of placed tags equals the expected total
        int placedTotal = foundA.Count + foundB.Count + foundC.Count;
        int expectedTotal = (CaseA?.Length ?? 0) + (CaseB?.Length ?? 0) + (CaseC?.Length ?? 0);
        if (expectedTotal > 0 && placedTotal >= expectedTotal)
        {
            Evaluate();
        }
    }

    // Helper used by existing raycast-based code if desired.
    // Returns true if the raycast's collider tag was handled by any case.
    public bool CheckTag(RaycastHit hit, CaseId caseId)
    {
        if (hit.collider == null) return false;
        RegisterTagForCase(hit.collider.tag, caseId);
        return true;
    }

    bool IsTagInArray(string tag, string[] arr)
    {
        if (arr == null) return false;
        for (int i = 0; i < arr.Length; i++)
        {
            if (tag == arr[i]) return true;
        }
        return false;
    }

    // Evaluate correctness across all placed tags and show result
    public void Evaluate()
    {
        int correct = 0;
        correct += CountCorrect(foundA, CaseA);
        correct += CountCorrect(foundB, CaseB);
        correct += CountCorrect(foundC, CaseC);

        int expectedTotal = (CaseA?.Length ?? 0) + (CaseB?.Length ?? 0) + (CaseC?.Length ?? 0);
        Debug.Log($"Result: {correct} correct out of {expectedTotal} expected.");

        if (correct >= expectedTotal && expectedTotal > 0)
        {
            if (Solved != null) Solved.SetActive(true);
            if (Unsolved != null) Unsolved.SetActive(false);
            Debug.Log("Good job! All tags are correct.");
        }
        else
        {
            if (Solved != null) Solved.SetActive(false);
            if (Unsolved != null) Unsolved.SetActive(true);
            Debug.Log("Some tags are incorrect. Keep trying.");
        }
    }

    int CountCorrect(HashSet<string> found, string[] expected)
    {
        if (found == null || expected == null) return 0;
        int count = 0;
        for (int i = 0; i < expected.Length; i++)
        {
            if (found.Contains(expected[i])) count++;
        }
        return count;
    }

    // Reset all progress at runtime
    public void ResetFoundTags()
    {
        foundA.Clear();
        foundB.Clear();
        foundC.Clear();
        Debug.Log("Tag progress reset.");
        if (Solved != null) Solved.SetActive(false);
        if (Unsolved != null) Unsolved.SetActive(true);
    }
}
