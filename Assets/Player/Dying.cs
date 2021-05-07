using UnityEngine;

[RequireComponent(typeof(HookThrowing))]
public class Dying : MonoBehaviour
{
    private Rigidbody2D rb;
    private HookThrowing hookThrowing;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hookThrowing = GetComponent<HookThrowing>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ISLAND_TAG))
        {
            if (!hookThrowing.MovingToHookPosition)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("DIE");
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;
    }
}
