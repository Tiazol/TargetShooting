using UnityEngine;

public class HookThrowing : MonoBehaviour
{
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform hookPoint;
    [SerializeField] private float throwingSpeed = 20f;

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
        Hook hook = hookGameObject.GetComponent<Hook>();
        hook.SpawnPoint = hookPoint;
        hook.ThrowingSpeed = throwingSpeed;

        hookGameObject.GetComponent<Rigidbody2D>().AddForce(hookPoint.right * throwingSpeed, ForceMode2D.Impulse);
    }
}
