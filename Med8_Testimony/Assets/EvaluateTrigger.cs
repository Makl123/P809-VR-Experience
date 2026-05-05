using UnityEngine;

public class EvaluateTrigger : MonoBehaviour
{
    [SerializeField] CaseSolver targetSolver;
    [SerializeField] string[] handTags = new string[] { "LeftHand", "RightHand", "XRHand" };
    [SerializeField] LayerMask handLayerMask = 0;

    // When an XR hand/controller enters this trigger, call Evaluate() on the solver.
    void OnTriggerEnter(Collider other)
    {
        if (targetSolver == null || other == null) return;
        if (IsHandCollider(other))
        {
            targetSolver.Evaluate();
        }
    }

    bool IsHandCollider(Collider other)
    {
        if (other == null) return false;

        if (handTags != null)
        {
            for (int i = 0; i < handTags.Length; i++)
            {
                var t = handTags[i];
                if (!string.IsNullOrEmpty(t) && other.CompareTag(t)) return true;
            }
        }

        if (handLayerMask != 0)
        {
            if (((1 << other.gameObject.layer) & handLayerMask.value) != 0) return true;
        }

        return false;
    }
}