using UnityEngine;

public class EEGInterpreter : MonoBehaviour
{
    [Header("Dependencies")]
    public UnicornReceiver receiver;
    public EEGFrequencyManager freqManager;

    [Header("Frequency Outputs")]
    public float alphaPower;
    public float betaPower;
    public float gammaPower;

    [Header("Immersion & Emotional Indicators")]
    [Range(0, 100)] public float stressLevel;      // High Beta / Alpha
    [Range(0, 100)] public float engagementLevel;  // Beta + Gamma / Theta
    public bool isBlinking;

    [Header("Settings")]
    public float blinkThreshold = 150f;

    void Update()
    {
        if (freqManager == null || receiver == null) return;

        // 1. Pull values from the Frequency Manager
        // We sync these so they show up in this component's Inspector too
        alphaPower = freqManager.alpha;
        betaPower = freqManager.beta;
        gammaPower = freqManager.gamma;

        // 2. Calculate Engagement (Immersion Metric)
        // High engagement = High Beta compared to Alpha/Theta
        if (freqManager.alpha > 0)
        {
            float engagementRaw = (freqManager.beta + freqManager.gamma) / (freqManager.alpha + freqManager.theta);
            engagementLevel = Mathf.Clamp(engagementRaw * 10f, 0, 100);
        }

        // 3. Calculate Stress (Cognitive Load)
        if (freqManager.alpha > 0)
        {
            float stressRaw = freqManager.beta / freqManager.alpha;
            stressLevel = Mathf.Clamp(stressRaw * 5f, 0, 100);
        }

        // 4. Blink Detection (using raw smoothed data for spikes)
        float[] smoothedData = receiver.GetSmoothedData();
        if (smoothedData != null && smoothedData.Length > 0)
        {
            isBlinking = Mathf.Abs(smoothedData[0]) > blinkThreshold;
        }
    }
}