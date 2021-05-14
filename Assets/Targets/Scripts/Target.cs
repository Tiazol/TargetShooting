using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(SpriteRenderer), typeof(Collider2D))]
public class Target : MonoBehaviour, IDestroyable
{
    public bool IsDestroying { get; private set; }

    private ParticleSystem particles;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2d;

    public event System.Action Destroying;

    private void Awake()
    {
        particles = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2d = GetComponent<Collider2D>();
    }

    public void DestroySelf()
    {
        IsDestroying = true;
        spriteRenderer.enabled = false;
        collider2d.enabled = false;
        Destroying?.Invoke();
        particles.Play();
        Destroy(gameObject, particles.main.duration + particles.main.startLifetimeMultiplier);
    }
}
