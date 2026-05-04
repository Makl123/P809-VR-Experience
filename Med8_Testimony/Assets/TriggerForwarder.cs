using UnityEngine;

public class TriggerForwarder : MonoBehaviour
{
    [SerializeField] CaseSolver targetSolver;
    [SerializeField] CaseSolver.CaseId caseId = CaseSolver.CaseId.A;

    private void OnTriggerEnter(Collider other)
    {
        if (targetSolver == null || other == null) return;
        targetSolver.RegisterTagForCase(other.tag, caseId);
    }
}