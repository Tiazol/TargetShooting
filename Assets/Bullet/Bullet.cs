using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Damage { get; set; } = 100f;

    [SerializeField, Range(0.1f, 10f)] private float destroyTime = 1f;

    private void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.TARGET_TAG))
        {
            var target = collision.gameObject.GetComponent<Target>();
            var health = target.GetComponent<Health>();
            if (!target.IsDestroying)
            {
                ScoreManager.Instance.IncreaseScore();
                health.CurrentValue -= Damage;
                Destroy(gameObject);
            }
        }
    }
}
