﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;
    [SerializeField, Range(0.5f, 3f)] private float spawningTime = 1f;
    [SerializeField, Range(1, 10)] private int spawningLimit = 5;
    private int targetsCount;
    private bool spawning = true;

    private void Start()
    {
        StartCoroutine(SpawnTargets());
    }

    IEnumerator SpawnTargets()
    {
        while (spawning)
        {
            yield return new WaitForSeconds(spawningTime);

            if (targetsCount < spawningLimit)
            {
                var x = Random.Range(-GameField.Instance.HorizontalLimit, GameField.Instance.HorizontalLimit);
                var y = Random.Range(-GameField.Instance.VerticalLimit, GameField.Instance.VerticalLimit);
                var spawnPosition = new Vector3(x, y, 0);

                var target = Instantiate(targetPrefab, spawnPosition, Quaternion.identity, transform);
                target.GetComponent<Target>().Destroyed += () => targetsCount--;
                targetsCount++;
            }
        }

        yield return null;
    }
}
