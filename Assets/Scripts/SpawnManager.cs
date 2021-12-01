using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;

    // Start is called before the first frame update

    private bool _stopSpawning = false;
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    IEnumerator SpawnEnemyRoutine()
    {

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(11.1f, Random.Range(-3.4f, 5.8f), 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.Euler(new Vector3(0, 0, 90)));
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

        }
    }

     IEnumerator SpawnPowerupRoutine(){

        while(_stopSpawning == false){
            Vector3 posToSpawn = new Vector3(11.1f, Random.Range(-3.4f, 5.8f),0);
            int randomPowerUp = Random.Range(0,2);
            Instantiate(powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(10.0f);

        }
    }

     public void onPalyerDeath()
    {
        _stopSpawning = true;
    }

}
