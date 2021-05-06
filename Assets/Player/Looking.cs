using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
    public class Looking : MonoBehaviour
{
    private Camera mainCamera;
    private Rigidbody2D rb;
    private Vector2 cursorPosition;

    private void Awake()
    {
        mainCamera = Camera.main;
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        cursorPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
    }

    private void FixedUpdate()
    {
        Vector2 lookDirection = cursorPosition - rb.position;
        float rotation = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        rb.rotation = rotation;
    }
}
