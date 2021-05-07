using System.Collections;

using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class HookThrowing : MonoBehaviour
{
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform hookPoint;
    [SerializeField] private float throwingSpeed = 20f;
    private Rigidbody2D rb;
    private Hook currentHook;
    
    public bool MovingToHookPosition { get; private set; }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThrowHook();
        }
    }

    private void ThrowHook()
    {
        GameObject hookGameObject = Instantiate(hookPrefab, hookPoint.position, hookPoint.rotation, transform);
        currentHook = hookGameObject.GetComponent<Hook>();
        currentHook.SpawnPoint = hookPoint;
        currentHook.ThrowingSpeed = throwingSpeed;
        currentHook.NewIslandFound += () => StartCoroutine(MoveToHookPosition());

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

        while (Vector3.Distance(currentHook.transform.position, transform.position) > 0.5f)
        {
            var direction = (currentHook.transform.position - transform.position).normalized;
            rb.MovePosition(transform.position + direction * Time.fixedDeltaTime * throwingSpeed);

            yield return new WaitForFixedUpdate();
        }

        Destroy(currentHook.gameObject);
        currentHook = null;

        if (movingComponent != null)
        {
            movingComponent.enabled = true;
        }

        MovingToHookPosition = false;

        yield return null;
    }
}
