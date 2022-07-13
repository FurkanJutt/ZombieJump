using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyReference;
    [SerializeField] Transform leftPos, rightPos;
    private GameObject spawnedEnemy;
    private int randomIndex, randowmSide;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(1, 7));

            randomIndex = Random.Range(0, enemyReference.Length);
            randowmSide = Random.Range(0, 2);

            spawnedEnemy = Instantiate(enemyReference[randomIndex]);

            // left side
            if (randowmSide == 0)
            {
                spawnedEnemy.transform.position = leftPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = Random.Range(2f, 5f);
            }
            else
            {
                // right side
                spawnedEnemy.transform.position = rightPos.position;
                spawnedEnemy.GetComponent<Enemy>().speed = -Random.Range(2f, 5f);
                spawnedEnemy.transform.localScale = new Vector3(-8f, 8f, 8f);
            }
        }// while loop
    }
}
