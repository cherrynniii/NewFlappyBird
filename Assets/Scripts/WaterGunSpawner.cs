using UnityEngine;

public class WaterGunSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterGunPrefab;

    private float spawnInterval = 8f;

    private float spawnX = 3.38f;
    private float minY = -2.8f;
    private float maxY = 4.42f;
    private float spawnZ = 0f;

    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {
        if (Score.score >= Score.goalScore)
        {
            timer += Time.deltaTime;
            if (timer >= spawnInterval)
            {
                SpawnWaterGun();
                timer = 0f;
            }
        }
    }

    private void SpawnWaterGun()
    {
        float y = Random.Range(minY, maxY);
        Vector3 pos = new Vector3(spawnX, y, spawnZ);
        Instantiate(waterGunPrefab, pos, Quaternion.identity);
        return;
    }
}
