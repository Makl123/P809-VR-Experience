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

    // References to CaseFileText components (assign the GameObjects that hold the CaseFileText script)
    [SerializeField] CaseFileText CaseFileA;
    [SerializeField] CaseFileText CaseFileB;
    [SerializeField] CaseFileText CaseFileC;

    // Saved case file strings for external reference (populated by Evaluate or SetCaseFileText)
    string[] caseFileTexts;
    public string[] CaseFileTexts => caseFileTexts;

    // Track placed tags per case (prevents double-counting same tag in a case)
    readonly HashSet<string> foundA = new HashSet<string>();
    readonly HashSet<string> foundB = new HashSet<string>();
    readonly HashSet<string> foundC = new HashSet<string>();

    // Last evaluated score for external queries
    int lastScore;
    public int LastScore => lastScore;

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

        void Update()
        {   
            if (newlyAdded)
            {
                Debug.Log($"Case {caseId}: placed tag '{tag}' -> {(correct ? "Correct" : "Incorrect")}");
                if (correct && YouDidIt != null) YouDidIt.Play();
            }
        } 

        Update();
    }

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

        // Preserve last score
        lastScore = correct;

        // Populate the caseFileTexts array so other scripts can read the saved strings.
        if (caseFileTexts == null) caseFileTexts = new string[3];
        caseFileTexts[(int)CaseId.A] = CaseFileA?.caseFileText ?? caseFileTexts[(int)CaseId.A] ?? string.Empty;
        caseFileTexts[(int)CaseId.B] = CaseFileB?.caseFileText ?? caseFileTexts[(int)CaseId.B] ?? string.Empty;
        caseFileTexts[(int)CaseId.C] = CaseFileC?.caseFileText ?? caseFileTexts[(int)CaseId.C] ?? string.Empty;
    }

    // New: allow external registration of the case file string for a case
    public void SetCaseFileText(CaseId caseId, string text)
    {
        if (caseFileTexts == null) caseFileTexts = new string[3];
        caseFileTexts[(int)caseId] = text ?? string.Empty;
        Debug.Log($"CaseSolver: SetCaseFileText Case {caseId} -> '{caseFileTexts[(int)caseId]}'");
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