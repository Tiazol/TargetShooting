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
        if (!PauseManager.IsPaused)
        {
            movementDirection.x = Input.GetAxis("Horizontal");
            movementDirection.y = Input.GetAxis("Vertical"); 
        }
    }

    private void FixedUpdate()
    {
        if (movementDirection.magnitude != 0)
        {
            animator.SetBool(AnimationParameters.CHARACTER_WALKING, true);
            rb.AddForce(movementDirection * movementSpeed);
        }
        else
        {
            animator.SetBool(AnimationParameters.CHARACTER_WALKING, false);
            rb.velocity = Vector2.zero;
        }
    }
}
