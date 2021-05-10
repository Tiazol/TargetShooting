using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(SpriteRenderer), typeof(Collider2D))]
public class Target : MonoBehaviour
{
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;
    Collider2D collider2d;

    public event System.Action Destroying;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    public void Destroy()
    {
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
        particles.Play();
        Destroying?.Invoke();
        Destroy(gameObject, particles.main.duration + particles.main.startLifetimeMultiplier);
    }
}
