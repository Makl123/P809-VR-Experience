using UnityEngine;
using MathNet.Numerics;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using System.Linq; // Only one of these!

public class EEGFrequencyManager : MonoBehaviour
{
    public UnicornReceiver receiver;

    [Header("Frequency Bands (Normalized 0-100)")]
    public float delta; public float theta; public float alpha;
    public float mu; public float beta; public float gamma;

    private const int SampleCount = 256;
    private Complex[] fftBuffer = new Complex[SampleCount];
    private float[] timeBuffer = new float[SampleCount];
    private int bufferIndex = 0;

    void Update()
    {
        if (receiver == null) return;
        float[] data = receiver.GetSmoothedData();
        if (data == null || data.Length == 0) return;

        if (bufferIndex < SampleCount)
        {
            timeBuffer[bufferIndex] = data[0];
            bufferIndex++;
        }
        else
        {
            ProcessFFT();
            bufferIndex = 0;
        }
    }

    void ProcessFFT()
    {
        float average = timeBuffer.Average();
        for (int i = 0; i < SampleCount; i++)
        {
            float window = 0.5f * (1f - Mathf.Cos(2f * Mathf.PI * i / (SampleCount - 1)));
            fftBuffer[i] = new Complex((timeBuffer[i] - average) * window, 0);
        }

        Fourier.Forward(fftBuffer, FourierOptions.NoScaling);

        delta = CalculatePower(0.5f, 4f);
        theta = CalculatePower(4f, 8f);
        alpha = CalculatePower(8f, 13f);
        mu = CalculatePower(9f, 11f);
        beta = CalculatePower(13f, 30f);
        gamma = CalculatePower(30f, 45f);
    }

    float CalculatePower(float low, float high)
    {
        float hzPerBin = 250f / SampleCount;
        int startBin = Mathf.FloorToInt(low / hzPerBin);
        int endBin = Mathf.FloorToInt(high / hzPerBin);
        float internalSum = 0; // Fixed name to avoid conflict

        for (int i = startBin; i <= endBin; i++)
        {
            if (i < fftBuffer.Length)
                internalSum += (float)fftBuffer[i].Magnitude;
        }
        return (internalSum / (Mathf.Max(1, endBin - startBin + 1))) / 1000f;
    }
}