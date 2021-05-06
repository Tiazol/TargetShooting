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
        ScoreManager.Instance.IncreaseScore();
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}
