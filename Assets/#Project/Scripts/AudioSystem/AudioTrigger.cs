using UnityEngine;

public class AudioTrigger : MonoBehaviour
{
    private bool recordingPossible;
    private Recorder recorder;
    private bool recordIsOn = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            recordingPossible = true;
            recorder = collision.GetComponent<Recorder>();
            recorder.audioTrigger = this;
            // Debug.Log("Player entered CrowTrigger");
        }
    }

    public void OnRecordButtonPressed()
    {
        if (recordingPossible && !recordIsOn)
        {
            // Debug.Log("RecordButton pressed");
            recorder.Record(gameObject.GetComponent<AudioSource>());
            recordIsOn = true;
        }
        else if (recordIsOn)
        {
            recorder.StopRecording();
            recordIsOn = false;
        }
    }
}
