using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Wavespawner : MonoBehaviour
{
    public Transform enemyPrefab;

    public Transform spawnpoint;

    public float timeBetweenWaves = 15.25f;
    private float countdown = 0.5f;

    public Text WaveCountdownText;

    private int WaveIndex = 0;
    void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        WaveCountdownText.text = string.Format("{0:00.00}", countdown);
    }
     IEnumerator SpawnWave ()
    {
       for (int i = 0; i < WaveIndex; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.5f);
            //afstand tussen de enemy's
        }
        
        WaveIndex++;
    }
    void SpawnEnemy ()
    {
        Instantiate(enemyPrefab, spawnpoint.position, spawnpoint.rotation);
    }
}
