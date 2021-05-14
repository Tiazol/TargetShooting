using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TargetsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField, Range(0.5f, 3f)] private float spawningTime = 1f;
    [SerializeField, Range(1, 10)] private int spawningLimit = 5;
    private Island island;
    private Stack<Target> targets;
    private bool spawning = true;

    private void Start()
    {
        island = GetComponentInParent<Island>();
        if (island == null)
        {
            Debug.LogWarning($"Warning: this {nameof(TargetsSpawner)} can't find any {nameof(Island)} component to spawn {nameof(Target)}s", this);
            return;
        }

        targets = new Stack<Target>(spawningLimit);
        for (int i = 0; i < spawningLimit; i++)
        {
            var instantiatedTarget = Instantiate(targetPrefab, transform.position, Quaternion.identity, transform).GetComponent<Target>();
            instantiatedTarget.gameObject.SetActive(false);
            instantiatedTarget.Spawner = this;
            targets.Push(instantiatedTarget);
        }

        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        var islandBounds = island.Bounds;

        while (spawning)
        {
            yield return new WaitForSeconds(spawningTime);

            if (targets.Count > 0)
            {
                var x = Random.Range(islandBounds.min.x, islandBounds.max.x);
                var y = Random.Range(islandBounds.min.y, islandBounds.max.y);
                GetInstantiatedTarget().transform.position = new Vector3(x, y, 0);
            }
        }

        yield break;
    }

    public Target GetInstantiatedTarget()
    {
        var target = targets.Pop();
        target.gameObject.SetActive(true);
        return target;
    }

    public void ReturnTargetToSpawner(Target target)
    {
        target.gameObject.SetActive(false);
        target.transform.position = transform.position;
        targets.Push(target);
    }
}
