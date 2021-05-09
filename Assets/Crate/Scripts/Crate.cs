using UnityEngine;

public class Crate : MonoBehaviour
{
    [SerializeField, Range(1, 100)] private int points = 10;
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;

    public int Points => points;
    public event System.Action Destroying;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Destroy()
    {
        spriteRenderer.enabled = false;
        particles.Play();
        Destroying?.Invoke();
        Destroy(gameObject, particles.main.duration + particles.main.startLifetimeMultiplier);
    }
}
