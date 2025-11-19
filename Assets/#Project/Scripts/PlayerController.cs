using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControler : MonoBehaviour
{
    [SerializeField] private D1Launcher dialogueLauncher;
    // Pour les contrôles, of course :
    [SerializeField] private InputActionAsset actions;
    // [SerializeField] private float speed;
    // private InputAction yAxis;
    // private InputAction xAxis;


    void Awake()
    {
        // xAxis = actions.FindActionMap("Player").FindAction("MoveX");
        // yAxis = actions.FindActionMap("Player").FindAction("MoveY");
        dialogueLauncher = GetComponent<D1Launcher>();
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

    // void Update()
    // {
    //     MoveX();
    //     MoveY();
    // }

    // private void MoveX()
    // {
    //     transform.Translate(xAxis.ReadValue<float>() * speed * Time.deltaTime, 0f, 0f);
    // }
    // private void MoveY()
    // {
    //     transform.Translate(0f, yAxis.ReadValue<float>() * speed * Time.deltaTime, 0f);
    // }

    private void OnInteract(InputAction.CallbackContext context)
    {
        dialogueLauncher.ButtonInteractPressed();
    }

}