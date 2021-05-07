using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Moving : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        movementDirection.x = Input.GetAxisRaw("Horizontal");
        movementDirection.y = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (movementDirection.magnitude != 0)
        {
            var movement = movementDirection * movementSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }
}
