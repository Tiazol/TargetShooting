using System.Collections;
using UnityEngine;

public class TargetsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField, Range(0.5f, 3f)] private float spawningTime = 1f;
    [SerializeField, Range(1, 10)] private int spawningLimit = 5;
    private Island island;
    private int targetsCount;
    private bool spawning = true;

    private void Start()
    {
        island = GetComponentInParent<Island>();
        if (island == null)
        {
            Debug.LogWarning($"Warning: this {nameof(TargetsSpawner)} can't find any {nameof(Island)} component to spawn {nameof(Target)}s", this);
            return;
        }

        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        var islandBounds = island.Bounds;

        while (spawning)
        {
            yield return new WaitForSeconds(spawningTime);

            if (targetsCount < spawningLimit)
            {
                var x = Random.Range(islandBounds.min.x, islandBounds.max.x);
                var y = Random.Range(islandBounds.min.y, islandBounds.max.y);
                var spawnPosition = new Vector3(x, y, 0);
                Debug.Log(targetsCount);
                var target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity, transform);
                target.GetComponent<Target>().Destroying += () => targetsCount--;
                targetsCount++;
            }
        }

        yield return null;
    }
}
