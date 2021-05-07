using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Island : MonoBehaviour
{
    public static Island Instance { get; private set; }

    public float HorizontalLimit { get; private set; }
    public float VerticalLimit { get; private set; }

    private void Awake()
    {
        Instance = this;

        var bounds = GetComponent<SpriteRenderer>().bounds;
        HorizontalLimit = Mathf.Abs(bounds.center.x - bounds.extents.x);
        VerticalLimit = Mathf.Abs(bounds.center.y - bounds.extents.y);
    }
}
