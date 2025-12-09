using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Recorder : MonoBehaviour
{
    // Moyen le plus simple de faire passer un audioTrigger au Recorder. Mais est-ce que c'est un problème ? Sachant que par la suite la classe sera probablement statique, je dirais que ça passe :
    [HideInInspector] public AudioTrigger audioTrigger;


    private Vector3 audioToPlayer;
    private AudioSource trackToPlay; 
    private AudioSource recordedTrack; //Voir Style 1 
    private Tape recordedTape; 
    private float delay;
    private bool isPlaying;

    // VARIABLES POUR STYLE 1, c'est à dire avant l'invention des tapes.
    private AudioSource audioSource;
    private float sampleStartTime;
    private float sampleEndTime; // For debug only
    private float unityStartTime; 
    private float unityEndTime;

    

    public void OnRecordButtonPressed()
    {
        if(audioTrigger != null)
        {
            audioTrigger.OnRecordButtonPressed();
        }
    }
    public void Record(AudioSource audioSource)
    {
        // // STYLE 1
        // sampleStartTime = audioSource.time;
        // unityStartTime = Time.time;
        // this.audioSource = audioSource;
        // audioToPlayer = this.audioSource.transform.position - gameObject.transform.position;

        // STYLE 2

        Tape tape = new Tape();
        tape.audioSource = audioSource;
        tape.sampleStartTime = audioSource.time;
        tape.unityStartTime = Time.time; // Le temps qu'il était dans Unity quand l'enregistrement de l'audioSource a commencé.
        // this.audioSource = audioSource; // Utile pour Style 1
        audioToPlayer = audioSource.transform.position - gameObject.transform.position;
        tape.distanceToPlayer = audioToPlayer;
        TrackList.tapes.Add(tape);
        Debug.Log("piste enregistrée : " + tape.audioSource.clip);
    }

    public void StopRecording()
    {
        // // SYLE 1
        // unityEndTime = Time.time;
        // // Pour debug uniquement :
        // sampleEndTime = audioSource.time;
        // Debug.Log("EndTime of the sample : " + sampleEndTime);

        // STYLE 2
        Debug.Log("Entered StopRecording()");
        if (TrackList.tapes.Count() != 0)
        {
        TrackList.tapes.Last().unityEndTime = Time.time; // Le temps qu'il était dans Unity quand l'enregistrement de l'audioSource s'est terminé.
        Debug.Log("clip enregistré dans Tracklist sous l'index Last() : " + TrackList.tapes.Last().audioSource.clip);
        }
    }

    public void PlayRecord(int buttonIndex)
    {
        // // STYLE 1
        // if (audioSource != null && !isPlaying && recordedTrack == null)
        // {
        //     // Debug.Log("Entered PlayRecord()");
        //     recordedTrack = Instantiate(audioSource);
        //     recordedTrack.time = sampleStartTime;
        //     recordedTrack.transform.position = gameObject.transform.position + audioToPlayer;

        //     recordedTrack.Play();
        //     isPlaying = true;
        //     StartCoroutine(EndOfSample());
        //     // Debug.Log("entered PlayRecord()");
        // }

        // STYLE 2
        if (TrackList.tapes != null && buttonIndex < TrackList.tapes.Count)
        {
        recordedTape = TrackList.tapes[buttonIndex];
        Debug.Log("Le buttonIndex est : " + buttonIndex);
        }

        if (recordedTape.audioSource != null && !isPlaying && trackToPlay == null) // Conditions à revoir.
        {

            Debug.Log("Entered PlayRecord(), recordedTape.audioSource.clip = " + recordedTape.audioSource.clip);

            // Après ça il reste du boulot pour tout "traduire" en tape (Parce que ci-dessous, c'est fait directement sur l'audioSource passée).
            trackToPlay = Instantiate(recordedTape.audioSource);
            trackToPlay.time = recordedTape.sampleStartTime;
            trackToPlay.transform.position = gameObject.transform.position + recordedTape.distanceToPlayer;

            trackToPlay.Play();
            isPlaying = true;
            StartCoroutine(EndOfSample(recordedTape));
            // Debug.Log("entered PlayRecord()");
        }

        // // CONDITION POUR ARRETER DE JOUER L'ENREGISTREMENT, pas encore au point. Il faudrait probablement utiliser StopCoroutine().
        // else if (audioSource != null && isPlaying && recordedTrack != null) 
        // {
        //     recordedTrack.Stop();
        //     Destroy(recordedTrack);
        //     isPlaying = false;
        //     // Debug.Log("Entered audioSource.Stop() section");
        // }
    }

    private IEnumerator EndOfSample(Tape currentTape)
    {
        // STYLE 1
        delay = currentTape.unityEndTime - currentTape.unityStartTime;
        yield return new WaitForSeconds(delay);
        trackToPlay.Stop();
        Destroy(trackToPlay);
        isPlaying = false;
        delay = 0;
    }
}
