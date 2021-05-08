using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Moving : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 movementDirection;
    private Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!PauseManager.Instance.IsPaused)
        {
            movementDirection.x = Input.GetAxisRaw("Horizontal");
            movementDirection.y = Input.GetAxisRaw("Vertical"); 
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection.magnitude != 0)
        {
            animator.SetBool("Walking", true);
            var movement = movementDirection * movementSpeed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
        else
        {
            animator.SetBool("Walking", false);
        }
    }
}
