using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChildSpawner : MonoBehaviour
{
    public static ChildSpawner instance;
    public List<GameObject> spawnPoints;
    public GameObject childPrefab;
    public int currentChildCount = 0;
    public int childSpawnLimit = 25;

    void Start()
    {
        for (int i = 0; i < childSpawnLimit; i++)
        {
            if (currentChildCount != childSpawnLimit)
            {
                SpawnChildren();
            }
        }
    }

    public void SpawnChildren()
    {
        if (currentChildCount != childSpawnLimit)
        {
            var child = Instantiate(childPrefab, spawnPoints[Random.Range(0, spawnPoints.Count)].transform);
            child.GetComponent<ChildController>().parent = this;
            child.GetComponent<ChildController>().destinationPoints = spawnPoints;
            currentChildCount += 1;
        }
    }
}
