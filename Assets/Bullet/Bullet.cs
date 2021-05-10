using UnityEngine;

public class Bullet : MonoBehaviour
{
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
            if (!target.IsDestroying)
            {
                ScoreManager.Instance.IncreaseScore();
                collision.gameObject.GetComponent<Target>().Destroy();
                Destroy(gameObject);
            }
        }
    }
}
