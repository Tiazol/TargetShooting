using UnityEngine;

public class Hook : MonoBehaviour
{
    private const string CRATE_TAG = "Crate";

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(CRATE_TAG))
        {
            var points = collision.gameObject.GetComponent<Crate>().Points;
            ScoreManager.Instance.IncreaseScoreBy(points);
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
