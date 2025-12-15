using Unity.VisualScripting;
using UnityEngine;

public class WoodsAudioBounds : MonoBehaviour
{
    [SerializeField] AudioTrigger ogreAudio;
    [SerializeField] AudioSource woodsAudio;

    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.transform.position.y < gameObject.transform.position.y)
        {
            // Debug.Log("Player is lower than woodsAudioBounds");
            ogreAudio.gameObject.SetActive(false);
            woodsAudio.Stop();
        }
        if(collision.CompareTag("Player") && collision.transform.position.y > gameObject.transform.position.y)
        {
            // Debug.Log("Player is higher than woodsAudioBounds");
            ogreAudio.gameObject.SetActive(true);
            woodsAudio.Play();
        }
    }

}
