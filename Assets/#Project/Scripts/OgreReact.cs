
using System.Collections;
using UnityEngine;

public class OgreReact : MonoBehaviour
{
    [SerializeField] private Recorder playerRecorder;
    [SerializeField] private float moveSpeed;
    [SerializeField] private GameObject fullOgre;
    public bool inOgreZone {get; private set;}

    private int ogreDirection;
    private Rigidbody2D player;
    private bool OgreMoves;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inOgreZone = true;
            player = collision.attachedRigidbody;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        inOgreZone = false;
        // Debug.Log("Player is out of Trigger2D");
    }

    public void OnOgreCutscene()
    {
        // Debug.Log("Ogre cutscene playing");
        if(player.position.x > this.transform.parent.position.x)
        {
            ogreDirection = -1;
        }
        else ogreDirection = 1;
        OgreMoves = true;
        StartCoroutine(OgreDeath());
    }

    void Update()
    {
        if (OgreMoves)
        {
            this.GetComponentInParent<Rigidbody2D>().MovePosition(transform.parent.position + new Vector3(20, 0, 0) * ogreDirection * Time.deltaTime * moveSpeed);
        }
    }

    private IEnumerator OgreDeath()
    {
        yield return new WaitForSeconds(5f);
        fullOgre.SetActive(false);
    }

}
