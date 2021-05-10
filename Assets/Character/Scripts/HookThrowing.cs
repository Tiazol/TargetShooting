using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HookThrowing : MonoBehaviour
{
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform hookPoint;
    [SerializeField, Range(10f, 30f)] private float throwingSpeed = 20f;
    [SerializeField, Range(1f, 10f)] private float throwingDistance = 3f;
    [SerializeField, Range(1f, 30f)] private float movingToHookSpeed = 10f;
    [SerializeField, Range(1f, 10f)] private float movingToHookDirectionMultiplayer = 1.3f;
    private Rigidbody2D rb;
    private Hook currentHook;
    private bool canThrow = true;
    
    public bool MovingToHookPosition { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!PauseManager.IsPaused)
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (canThrow)
                {
                    ThrowHook();
                }
            } 
        }
    }

    private void ThrowHook()
    {
        canThrow = false;

        GameObject hookGameObject = Instantiate(hookPrefab, hookPoint.position, hookPoint.rotation, transform);
        currentHook = hookGameObject.GetComponent<Hook>();
        currentHook.SpawnPoint = hookPoint;
        currentHook.ThrowingSpeed = throwingSpeed;
        currentHook.ThrowingDistance = throwingDistance;
        currentHook.NewIslandFound += () => StartCoroutine(MoveToHookPosition());
        currentHook.Destroying += () => canThrow = true;

        hookGameObject.GetComponent<Rigidbody2D>().AddForce(hookPoint.right * throwingSpeed, ForceMode2D.Impulse);
    }

    private IEnumerator MoveToHookPosition()
    {
        MovingToHookPosition = true;

        currentHook.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        var movingComponent = GetComponent<Moving>();
        if (movingComponent != null)
        {
            movingComponent.enabled = false;
        }

        var initialMovingDirection = currentHook.transform.position - transform.position;
        var multipliedMovingDirection = initialMovingDirection * movingToHookDirectionMultiplayer;
        var targetPosition = transform.position + multipliedMovingDirection;


        while ((targetPosition - transform.position).magnitude > 0.5f)
        {
            var direction = (targetPosition - transform.position).normalized;
            rb.AddForce(direction * movingToHookSpeed, ForceMode2D.Impulse);

            yield return new WaitForFixedUpdate();
        }

        Destroy(currentHook.gameObject);
        currentHook = null;
        MovingToHookPosition = false;
        rb.velocity = Vector2.zero;
        canThrow = true;

        if (movingComponent != null)
        {
            movingComponent.enabled = true;
        }

        yield return null;
    }
}
