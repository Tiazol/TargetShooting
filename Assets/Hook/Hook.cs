﻿using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Hook : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool outOfIsland;

    public Transform SpawnPoint { get; set; }
    public float ThrowingSpeed { get; set; } = 1f;

    public event System.Action NewIslandFound;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        if (SpawnPoint == null)
        {
            Debug.LogError("SpawnPoint not set!");
            Destroy(gameObject);
        }

        rb.AddForce(SpawnPoint.right * ThrowingSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.CRATE_TAG))
        {
            rb.rotation = SpawnPoint.rotation.eulerAngles.z;
            rb.freezeRotation = true;

            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(BringCrateToSpawnPoint(collision));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ISLAND_TAG))
        {
            if (outOfIsland)
            {
                Debug.Log("New Island!");
                NewIslandFound?.Invoke();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TagManager.ISLAND_TAG))
        {
            outOfIsland = true;
        }
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

        Destroy(collision.gameObject);
        Destroy(gameObject);

        yield return null;
    }
}
