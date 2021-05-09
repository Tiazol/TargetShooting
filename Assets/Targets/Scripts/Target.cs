using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(SpriteRenderer))]
public class Target : MonoBehaviour
{
    ParticleSystem particles;
    SpriteRenderer spriteRenderer;

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
