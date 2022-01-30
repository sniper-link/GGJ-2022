using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSpawner : MonoBehaviour
{
    public GameObject canPrefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CanSpawnDelay());
    }

    public void SpawnCan()
    {
        Instantiate(canPrefab, transform.position, Quaternion.identity);
    }

    IEnumerator CanSpawnDelay()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            SpawnCan();
        }
    }
}
