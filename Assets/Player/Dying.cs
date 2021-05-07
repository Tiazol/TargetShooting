using UnityEngine;

[RequireComponent(typeof(HookThrowing))]
public class Dying : MonoBehaviour
{
    private Rigidbody2D rb;
    private HookThrowing hookThrowing;
    private AudioSource audioSource;
    [SerializeField] private AudioClip dyingSound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        hookThrowing = GetComponent<HookThrowing>();
        audioSource = GetComponent<AudioSource>();
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
        transform.position = Vector3.zero;
        rb.velocity = Vector2.zero;

        if (audioSource != null)
        {
            if (dyingSound != null)
            {
                audioSource.PlayOneShot(dyingSound, 0.5f);
            }
        }
    }
}
