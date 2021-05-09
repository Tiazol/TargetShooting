using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject lastIsland;
    private bool isOutOfIsland;
    private bool isNewIslandFound;

    private AudioSource audioSource;
    [SerializeField] private AudioClip hookSound;

    public Transform SpawnPoint { get; set; }
    public float ThrowingSpeed { get; set; } = 1f;
    public float ThrowingDistance { get; set; } = 3f;

    public event System.Action NewIslandFound;
    public event System.Action Destroying;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        if (SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint not set!");
            Destroy(gameObject);
        }

        rb.AddForce(SpawnPoint.right * ThrowingSpeed, ForceMode2D.Impulse);

        if (audioSource != null)
        {
            if (hookSound != null)
            {
                audioSource.PlayOneShot(hookSound);
            }
        }
    }

    private void Update()
    {
        if (Vector3.Distance(SpawnPoint.transform.position, transform.position) > ThrowingDistance)
        {
            if (!isNewIslandFound)
            {
                StartCoroutine(ReturnToSpawnPoint()); 
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.CRATE_TAG))
        {
            rb.rotation = SpawnPoint.rotation.eulerAngles.z;
            rb.freezeRotation = true;

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(BringCrateToSpawnPoint(collision));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ISLAND_TAG))
        {
            if (lastIsland == null)
            {
                lastIsland = collision.gameObject;
                isOutOfIsland = false;
            }

            if (isOutOfIsland && (lastIsland != collision.gameObject))
            {
                lastIsland = collision.gameObject;
                isNewIslandFound = true;
                NewIslandFound?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.ISLAND_TAG))
        {
            isOutOfIsland = true;
        }
    }

    private IEnumerator ReturnToSpawnPoint()
    {
        while (Vector3.Distance(SpawnPoint.position, transform.position) > 0.5f)
        {
            var direction = (SpawnPoint.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * ThrowingSpeed);

            yield return new WaitForFixedUpdate();
        }

        Destroying?.Invoke();
        Destroy(gameObject);
    }

    private IEnumerator BringCrateToSpawnPoint(Collision2D collision)
    {
        while (Vector3.Distance(SpawnPoint.position, transform.position) > 0.5f)
        {
            var direction = (SpawnPoint.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * ThrowingSpeed);

            collision.transform.Translate(direction * Time.fixedDeltaTime * ThrowingSpeed);

            yield return new WaitForFixedUpdate();
        }

        var points = collision.gameObject.GetComponent<Crate>().Points;
        ScoreManager.Instance.IncreaseScoreBy(points);

        collision.gameObject.GetComponent<Crate>().Destroy();
        Destroying?.Invoke();
        Destroy(gameObject);
    }
}
