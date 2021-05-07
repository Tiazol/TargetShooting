using UnityEngine;

public class HookThrowing : MonoBehaviour
{
    [SerializeField] private GameObject hookPrefab;
    [SerializeField] private Transform hookPoint;
    [SerializeField] private float throwingSpeed = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            ThrowHook();
        }
    }

    private void ThrowHook()
    {
        GameObject hook = Instantiate(hookPrefab, hookPoint.position, hookPoint.rotation, transform);
        hook.GetComponent<Rigidbody2D>().AddForce(hookPoint.right * throwingSpeed, ForceMode2D.Impulse);
    }
}
