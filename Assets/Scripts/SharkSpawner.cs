using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject shark;
    bool bossSpawned = false;

    void Update()
    {
        if (!bossSpawned && Score.score > Score.goalScore)
        {
            bossSpawned = true;
            Invoke("SpawnShark", 3f); // 3초 뒤 실행
        }
    }

    void SpawnShark()
    {
        Instantiate(shark, new Vector3(5.26f, 0f, 0f), Quaternion.identity);
    }
}