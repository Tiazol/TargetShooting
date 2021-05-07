using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class IslandsManager : MonoBehaviour
{
    [SerializeField] private GameObject islandPrefab;
    private Island[] islands;
    private float horizontalLimit;
    private float verticalLimit;

    public static IslandsManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        var bounds = islandPrefab.GetComponent<SpriteRenderer>().bounds;
        horizontalLimit = Mathf.Abs(bounds.center.x - bounds.extents.x);
        verticalLimit = Mathf.Abs(bounds.center.y - bounds.extents.y);
    }

    private void Start()
    {
        islands = GetComponentsInChildren<Island>();
    }

    public (Vector2, Vector2) GetRandomIslandLimits()
    {
        int index = Random.Range(0, islands.Length);
        var randomIsland = islands[index];

        float hleft = randomIsland.transform.position.x - horizontalLimit;
        float hright = randomIsland.transform.position.x + horizontalLimit;

        float vbottom = randomIsland.transform.position.y - verticalLimit;
        float vtop = randomIsland.transform.position.y + verticalLimit;

        return (new Vector2(hleft, vbottom), new Vector2(hright, vtop));
    }
}
