using UnityEngine;
using TMPro;

public class EndGameResults : MonoBehaviour
{
    public CaseSolver CaseSolver;
    public TextMeshProUGUI ResultText;

    private int score;

    public void ShowResults()
    {
        // Get score safely
        score = CaseSolver != null ? CaseSolver.LastScore : 0;
        Debug.Log($"Final Score: {score}");

        // Get the three case strings safely (fall back to empty)
        string caseA = string.Empty;
        string caseB = string.Empty;
        string caseC = string.Empty;

        var texts = CaseSolver?.CaseFileTexts;
        if (texts != null)
        {
            if (texts.Length > 0) caseA = texts[0] ?? string.Empty;
            if (texts.Length > 1) caseB = texts[1] ?? string.Empty;
            if (texts.Length > 2) caseC = texts[2] ?? string.Empty;
        }

        // Build and assign the formatted result string
        if (ResultText != null)
        {
            ResultText.text =
                "                        Congratulations!\n\n" +
                $"You had {score} out of 3 correct\n" +
                $"Prisoner 1 Fate: {caseA}\n" +
                $"Prisoner 2 Fate: {caseB}\n" +
                $"Prisoner 3 Fate: {caseC}";
        }
        else
        {
            Debug.LogWarning("ResultText reference is missing on EndGameResults.");
        }
    }
}
