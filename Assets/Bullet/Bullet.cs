using System.Collections;

using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; } = 100f;
    public float Speed { get; set; } = 1f;
    public BulletsHolder Holder { get; set; }
    public event System.Action CollisionEntered;

    [SerializeField, Range(0.1f, 10f)] private float returnTime = 1f;
    [SerializeField] private Rigidbody2D rb;

    private void OnEnable()
    {
        StartCoroutine(ReturnToHolder());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.TARGET_TAG))
        {
            var target = collision.gameObject.GetComponent<Target>();
            var health = target.GetComponent<Health>();
            if (!target.IsDestroying)
            {
                health.CurrentValue -= Damage;
                CollisionEntered?.Invoke();
            }
            Holder.ReturnBulletToHolder(this);
        }
    }

    private IEnumerator ReturnToHolder()
    {
        yield return new WaitForSeconds(returnTime);
        Holder.ReturnBulletToHolder(this);
    }

    public void Run(Vector2 direction)
    {
        rb.AddForce(direction * Speed, ForceMode2D.Impulse);
    }
}
