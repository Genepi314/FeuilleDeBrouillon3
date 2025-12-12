using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] ByTileController player;
    private float cameraZ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        cameraZ = transform.position.z;
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, cameraZ);
    }

    void LateUpdate()
    {
        transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, cameraZ);
    }
}
