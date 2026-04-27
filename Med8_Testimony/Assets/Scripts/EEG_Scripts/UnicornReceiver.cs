using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class UnicornReceiver : MonoBehaviour
{
    public int port = 1000;
    [Range(0.01f, 1.0f)] public float smoothness = 0.15f;

    private UdpClient udpClient;
    private Thread receiveThread;
    private bool isRunning = true;

    private float[] rawEEGValues = new float[17];
    // Variable declaration must be here at the class level:
    private float[] dcOffset = new float[17];
    private float[] smoothedEEGValues = new float[17];
    private readonly object dataLock = new object();

    void Start()
    {
        receiveThread = new Thread(new ThreadStart(ListenForData));
        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void ListenForData()
    {
        try
        {
            udpClient = new UdpClient(port);
            IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
            while (isRunning)
            {
                byte[] data = udpClient.Receive(ref remoteEndPoint);
                if (data.Length == 68)
                {
                    float[] tempValues = new float[17];
                    for (int i = 0; i < 17; i++)
                        tempValues[i] = BitConverter.ToSingle(data, i * 4);

                    lock (dataLock) { rawEEGValues = tempValues; }
                }
            }
        }
        catch (Exception e) { if (isRunning) Debug.LogError(e.Message); }
    }

    void Update()
    {
        lock (dataLock)
        {
            for (int i = 0; i < 17; i++)
            {
                if (dcOffset[i] == 0 && rawEEGValues[i] != 0) dcOffset[i] = rawEEGValues[i];

                // Fast tracking to fight that 215k offset
                dcOffset[i] = Mathf.Lerp(dcOffset[i], rawEEGValues[i], 0.05f);
                float cleanSignal = rawEEGValues[i] - dcOffset[i];
                smoothedEEGValues[i] = Mathf.Lerp(smoothedEEGValues[i], cleanSignal, smoothness);
            }
        }
    }

    public float[] GetSmoothedData() { lock (dataLock) { return (float[])smoothedEEGValues.Clone(); } }

    private void OnDisable() => StopUDP();
    private void OnApplicationQuit() => StopUDP();
    private void StopUDP()
    {
        isRunning = false;
        udpClient?.Close();
        if (receiveThread != null && receiveThread.IsAlive) receiveThread.Abort();
    }
}