using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Island : MonoBehaviour
{
    public Bounds Bounds => spriteRenderer.bounds;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
