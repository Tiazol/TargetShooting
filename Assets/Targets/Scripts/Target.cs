using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Health), typeof(ParticleSystem))]
public class Target : MonoBehaviour, IDestroyable
{
    public TargetsSpawner Spawner { get; set; }
    public bool IsDestroying { get; private set; }

    private Health health;
    private ParticleSystem particles;

    public event System.Action Destroying;

    private void Awake()
    {
        health = GetComponent<Health>();
        particles = GetComponent<ParticleSystem>();
    }

    private IEnumerator ReturnToSpawner(float time)
    {
        yield return new WaitForSeconds(time);
        health.Restore();
        IsDestroying = false;
        Spawner.ReturnTargetToSpawner(this);
        yield break;
    }

    public void DestroySelf()
    {
        ScoreManager.Instance.IncreaseScore();

        IsDestroying = true;
        Destroying?.Invoke();
        particles.Play();

        StartCoroutine(ReturnToSpawner(particles.main.duration));
    }
}
