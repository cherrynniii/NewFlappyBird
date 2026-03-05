using UnityEngine;

public class BatterySpawner : MonoBehaviour
{
    [SerializeField] private GameObject batteryPrefab;
    [SerializeField] private Vector2 checkBoxSize = new Vector2(0.4f, 0.4f); // 배터리 크기 정도

    private float spawnInterval = 8f;

    private float minX = 3.38f;
    private float maxX = 5.47f;
    private float minY = -2.8f;
    private float maxY = 4.42f;
    private float spawnZ = 0f;

    private float timer = 0f;
    private int maxTry = 10;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            TrySpawnBattery();
            timer = 0f;
        }
    }

    private void TrySpawnBattery()
    {
        for (int i = 0; i < maxTry; i++)
        {
            float spawnX = Random.Range(minX, maxX);
            float y = Random.Range(minY, maxY);
            Vector3 pos = new Vector3(spawnX, y, spawnZ);

            // 주변 콜라이더 검사
            Collider2D[] hits = Physics2D.OverlapBoxAll(pos, checkBoxSize, 0f);

            bool overlapObstacle = false;

            foreach (var hit in hits)
            {
                if (hit.CompareTag("RealObstacle"))
                {
                    overlapObstacle = true;
                    break;
                }
            }

            if (!overlapObstacle)
            {
                Instantiate(batteryPrefab, pos, Quaternion.identity);
                return;
            }
        }
    }
}