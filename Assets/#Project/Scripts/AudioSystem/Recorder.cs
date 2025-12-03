using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Recorder : MonoBehaviour
{
    // Moyen le plus simple de faire passer un audioTrigger au Recorder. Mais est-ce que c'est un problème ? Sachant que par la suite la classe sera probablement statique, je dirais que ça passe :
    [HideInInspector] public AudioTrigger audioTrigger;

    private AudioSource audioSource;
    private AudioSource recordedTrack;
    private bool isPlaying;
    private Vector3 audioToPlayer;
    private float sampleStartTime;
    // For debug only :
    private float sampleEndTime;
    private float unityStartTime;
    private float unityEndTime;
    private float delay;

    public void OnRecordButtonPressed()
    {

        if(audioTrigger != null)
        {
            audioTrigger.OnRecordButtonPressed();
        }
    }
    public void Record(AudioSource audioSource)
    {
        sampleStartTime = audioSource.time;
        unityStartTime = Time.time;
        Debug.Log("StartTime of the sample  : " + sampleStartTime);
        this.audioSource = audioSource;
        audioToPlayer = this.audioSource.transform.position - gameObject.transform.position;
        // Debug.Log("audioSource passed to recorder, distance to player = " + audioToPlayer);
    }

    public void StopRecording()
    {
        unityEndTime = Time.time;
        sampleEndTime = audioSource.time;
        Debug.Log("EndTime of the sample : " + sampleEndTime);
    }

    public void PlayRecord()
    {
        if (audioSource != null && !isPlaying && recordedTrack == null)
        {
            // Debug.Log("Entered PlayRecord()");
            recordedTrack = Instantiate(audioSource);
            recordedTrack.time = sampleStartTime;
            recordedTrack.transform.position = gameObject.transform.position + audioToPlayer;

            recordedTrack.Play();
            isPlaying = true;
            StartCoroutine(EndOfSample());
            // Debug.Log("entered PlayRecord()");
        }

        // // ---La condition suivante devrait permettre au joueur d'arrêter de jouer l'audioClip s'il réappuie sur le bouton Play. Le problème, c'est que ça vient interférer avec la coroutine... Je n'ai pas encore trouvé comment résoudre ce souci, donc pour l'instant, le joueur est obligé d'écouter le clip qu'il a lancé jusqu'au bout---

        // else if (audioSource != null && isPlaying && recordedTrack != null) 
        // {
        //     recordedTrack.Stop();
        //     Destroy(recordedTrack);
        //     isPlaying = false;
        //     // Debug.Log("Entered audioSource.Stop() section");
        // }
    }

    private IEnumerator EndOfSample()
    {
        delay = unityEndTime - unityStartTime;
        yield return new WaitForSeconds(delay);
        recordedTrack.Stop();
        Destroy(recordedTrack);
        isPlaying = false;
        delay = 0;
    }
}
