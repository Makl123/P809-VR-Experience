using UnityEngine;

public class TriggerEnter : MonoBehaviour
{
    public GameObject player;
    public GameObject endPoint;

    public CaseSolver caseSolver;
    public EndGameResults endGameResults;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LeftHand"))
        {
            caseSolver.Evaluate();
            player.transform.position = endPoint.transform.position;
            endGameResults.ShowResults();
        }

    }
}
