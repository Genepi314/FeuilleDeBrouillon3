using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class TileController : MonoBehaviour
{
    [SerializeField] private InputActionAsset actions;
    // [SerializeField] private float speed;
    private InputAction yAxis;
    private InputAction xAxis;   
    private bool isMoving = false;
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private float timeToMove = 0.2f;
    void Awake()
    {
        xAxis = actions.FindActionMap("Player").FindAction("MoveX");
        yAxis = actions.FindActionMap("Player").FindAction("MoveY");
    }

    void OnEnable()
    {
        actions.FindActionMap("Player").Enable();
        actions.FindActionMap("Player").FindAction("Interact").performed += OnInteract;        
    }

    void OnDisable()
    {
        actions.FindActionMap("Player").Disable();
        actions.FindActionMap("Player").FindAction("Interact").performed -= OnInteract;
    }

    void Update()
    {
        if ((xAxis.ReadValue<float>() != 0 || yAxis.ReadValue<float>() != 0) && !isMoving)
        {
            StartCoroutine(Move());
        }
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        
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
}
