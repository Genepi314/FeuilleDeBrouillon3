using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class ByTileController : MonoBehaviour
{

    [SerializeField] private InputActionAsset actions;
    // private AudioTrigger audioTrigger;

    private Recorder recorder;
    private InputAction yAxis;
    private InputAction xAxis;   
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timeToMove = 0.15f;

    void Awake()
    {
        xAxis = actions.FindActionMap("Player").FindAction("MoveX");
        yAxis = actions.FindActionMap("Player").FindAction("MoveY");

        recorder = gameObject.GetComponent<Recorder>();
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        actions.FindActionMap("Player").FindAction("Interact").performed += OnInteract;        
        actions.FindActionMap("Player").FindAction("Record").performed += OnRecordButton;   
        actions.FindActionMap("Player").FindAction("Play").performed += OnPlayButton;   
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        actions.FindActionMap("Player").FindAction("Interact").performed -= OnInteract;
        actions.FindActionMap("Player").FindAction("Record").performed -= OnRecordButton;        
        actions.FindActionMap("Player").FindAction("Play").performed -= OnPlayButton;   
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

        if (xAxis.ReadValue<float>() > 0)
        {
            targetPosition = startPosition + Vector3.right;
        }
        else if (xAxis.ReadValue<float>() < 0)
        {
            targetPosition = startPosition - Vector3.right;
        }
        else if (yAxis.ReadValue<float>() > 0)
        {
            targetPosition = startPosition + Vector3.up;
        }
        else if (yAxis.ReadValue<float>() < 0)
        {
            targetPosition = startPosition - Vector3.up;
        }

        while(elapsedTime < timeToMove)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, (elapsedTime/timeToMove));
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition;

        isMoving = false;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        
    }

    private void OnRecordButton(InputAction.CallbackContext context)
    {
        recorder.OnRecordButtonPressed();
    }

    private void OnPlayButton(InputAction.CallbackContext context)
    {

        recorder.PlayRecord();
    }
}
