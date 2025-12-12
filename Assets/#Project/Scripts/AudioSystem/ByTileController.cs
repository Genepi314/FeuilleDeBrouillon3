using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ByTileController : MonoBehaviour
{
    // Linked GameObjects:
    [SerializeField] private InputActionAsset actions;
    private InputAction yAxis;
    private InputAction xAxis;   
    private Recorder recorder;

    // Variables for making the GRID-BASED movement:
    [SerializeField] private float timeToMove = 0.25f;
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 playerOffset;

    // TEST link UI:
    // [SerializeField] Comedon comedon;
    [SerializeField] TrackMenu trackMenu;

    // SCRIPT
    void Awake()
    {
        playerOffset = transform.position;
        xAxis = actions.FindActionMap("Player").FindAction("MoveX");
        yAxis = actions.FindActionMap("Player").FindAction("MoveY");

        recorder = gameObject.GetComponent<Recorder>();
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        actions.FindActionMap("Player").FindAction("Interact").performed += OnInteract;        
        actions.FindActionMap("Player").FindAction("Record").performed += OnRecordButton;   
        // actions.FindActionMap("Player").FindAction("Play").performed += OnPlayButton;   // Pour Recorder Style 1
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        actions.FindActionMap("Player").FindAction("Interact").performed -= OnInteract;
        actions.FindActionMap("Player").FindAction("Record").performed -= OnRecordButton;        
        // actions.FindActionMap("Player").FindAction("Play").performed -= OnPlayButton;   // Pour Recorder Style 1
    }

    void Update()
    {
        if ((xAxis.ReadValue<float>() != 0 || yAxis.ReadValue<float>() != 0) && !isMoving)
        {
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {

        isMoving = true;
        float elapsedTime = 0;

        startPosition = transform.position;
        Vector3 raycastStartPosition = new Vector3(startPosition.x + 0.5f, startPosition.y + 0.5f, 0); 

        if (xAxis.ReadValue<float>() > 0 && Physics2D.Raycast(raycastStartPosition, Vector3.right, 1f).collider == null)
        {
            targetPosition = startPosition + Vector3.right;    
        }
        else if (xAxis.ReadValue<float>() < 0 && Physics2D.Raycast(raycastStartPosition, Vector3.left, 1f).collider == null)
        {
            targetPosition = startPosition - Vector3.right;
        }
        else if (yAxis.ReadValue<float>() > 0 && Physics2D.Raycast(raycastStartPosition, Vector3.up, 1f).collider == null)
        {
            targetPosition = startPosition + Vector3.up;
        }
        else if (yAxis.ReadValue<float>() < 0 && Physics2D.Raycast(raycastStartPosition, Vector3.down, 1f).collider == null)
        {
            targetPosition = startPosition - Vector3.up;
        }
        else targetPosition = startPosition;


        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime/timeToMove);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }



    private void OnInteract(InputAction.CallbackContext context)
    {
        // comedon.OnMenuButtonPressed();
        trackMenu.OnMenuButtonPressed();

    }

    private void OnRecordButton(InputAction.CallbackContext context)
    {
        recorder.OnRecordButtonPressed();
    }

    // // Pour RECORDER Style 1:
    // private void OnPlayButton(InputAction.CallbackContext context)
    // {

    //     recorder.PlayRecord();
    // }
}
