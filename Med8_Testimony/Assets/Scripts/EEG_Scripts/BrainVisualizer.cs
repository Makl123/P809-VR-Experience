using UnityEngine;

public class BrainVisualizer : MonoBehaviour
{
    public EEGFrequencyManager freqManager;
    public float sensitivity = 0.01f;

    void Update()
    {
        // We use Alpha to control the size of the sphere
        // As you relax (Alpha goes up), the sphere grows
        float scale = 1.0f + (freqManager.alpha * sensitivity);
        transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(scale, scale, scale), Time.deltaTime * 5f);

        // Change color based on Beta (Focus/Stress)
        // Red = High Beta (Stressed), Blue = Low Beta (Calm)
        float stressColor = Mathf.Clamp01(freqManager.beta * sensitivity);
        GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, stressColor);
    }
}