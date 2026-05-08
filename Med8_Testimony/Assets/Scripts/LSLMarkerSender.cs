using UnityEngine;
using LSL;
public class LSLMarkerSender : MonoBehaviour
{
    StreamOutlet markerStream;
    [SerializeField] string MarkerName = "OpenSignals";
    [SerializeField] int hz = 1000;
    public static LSLMarkerSender Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        markerStream = new StreamOutlet(new StreamInfo("UnityMarkerStream", "Markers", 1, 0, channel_format_t.cf_string));
    }

    public void SendMarker(string marker)
    {
        markerStream.push_sample(new string[] { marker });
        print($"[LSL Marker Sender]: Marker sent: {marker}");
    }
}
