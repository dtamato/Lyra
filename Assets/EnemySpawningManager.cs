using UnityEngine;
using System.Collections;

public class EnemySpawningManager : MonoBehaviour {

    [SerializeField]
    private GameObject enemy;

    [SerializeField]
    private GameObject[] waypoints;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < waypoints.Length; i++)
            {
                Debug.Log("Spawning");
                GameObject spawnedEnemy = Instantiate(enemy, waypoints[i].transform.position, Quaternion.identity) as GameObject;
                Debug.Log(spawnedEnemy);
            }
            Destroy(gameObject.GetComponent<BoxCollider>());
        }
    }
}
