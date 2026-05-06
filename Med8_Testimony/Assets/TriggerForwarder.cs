using UnityEngine;

public class TriggerForwarder : MonoBehaviour
{
    [SerializeField] CaseSolver targetSolver;
    [SerializeField] CaseSolver.CaseId caseId = CaseSolver.CaseId.A;

    private void OnTriggerEnter(Collider other)
    {
        if (targetSolver == null || other == null) return;

        // If the colliding object has a CaseFileText component, register its string with CaseSolver.
        var caseFile = other.GetComponent<CaseFileText>() ?? other.GetComponentInChildren<CaseFileText>();
        if (caseFile != null)
        {
            targetSolver.SetCaseFileText(caseId, caseFile.caseFileText);
        }

        // Preserve existing behavior: register the object's tag for scoring
        targetSolver.RegisterTagForCase(other.tag, caseId);
    }
}