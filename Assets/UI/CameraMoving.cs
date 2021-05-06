using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField, Range(1f, 5f)] private float cameraMovingSpeed = 1f;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        var x = Player.Instance.transform.position.x;
        var y = Player.Instance.transform.position.y;
        var z = mainCamera.transform.position.z;

        var currentPosition = transform.position;
        var newPosition = new Vector3(x, y, z);

        mainCamera.transform.position = Vector3.Lerp(currentPosition, newPosition, Time.deltaTime * cameraMovingSpeed);
    }
}
