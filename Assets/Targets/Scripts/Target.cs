using System.Collections;

using TMPro;

using UnityEngine;

[RequireComponent(typeof(Health), typeof(ParticleSystem))]
public class Target : MonoBehaviour, IDestroyable
{
    public TargetsSpawner Spawner { get; set; }
    public bool IsDestroying { get; private set; }

    public event System.Action Destroying;

    private Health health;
    private ParticleSystem particles;
    private TextMeshPro healthText;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.CurrentValueChanged += newValue => StartCoroutine(OnHealthCurrentValueChanged(newValue));

        particles = GetComponent<ParticleSystem>();

        healthText = GetComponentInChildren<TextMeshPro>();
        healthText.gameObject.SetActive(false);
    }

    private IEnumerator OnHealthCurrentValueChanged(float newValue)
    {
        healthText.text = newValue.ToString();
        healthText.gameObject.SetActive(true);

        healthText.color = Color.red;
        yield return new WaitForSeconds(0.125f);
        healthText.color = Color.white;
        yield return new WaitForSeconds(0.125f);
        healthText.color = Color.red;
        yield return new WaitForSeconds(0.125f);
        healthText.color = Color.white;
        yield return new WaitForSeconds(0.125f);

        healthText.gameObject.SetActive(false);
    }

    private IEnumerator ReturnToSpawner(float time)
    {
        yield return new WaitForSeconds(time);
        health.Restore();
        healthText.gameObject.SetActive(false); 
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
