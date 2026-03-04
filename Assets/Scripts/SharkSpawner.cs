using UnityEngine;
using UnityEngine.UI;

public class SharkSpawner : MonoBehaviour
{
    public GameObject shark;
    bool bossSpawned = false;
    public Slider bossHpSlider;

    private PlayBGM playBGM;

    private void Start()
    {
        // PlayScene 안에 있는 PlayBGM 찾아두기
        playBGM = FindFirstObjectByType<PlayBGM>();
    }

    void Update()
    {
        if (!bossSpawned && Score.score >= Score.goalScore)
        {
            bossSpawned = true;
            if (playBGM != null)
                playBGM.PlayBoss();
            else
                Debug.LogWarning("PlayScene에 PlayBGM 오브젝트가 있는지 확인!");
            Invoke("SpawnShark", 3f); // 3초 뒤 실행
            Debug.Log("Spawner Update");
        }
    }

    void SpawnShark()
    {
        Instantiate(shark, new Vector3(5.26f, 0f, 0f), Quaternion.identity);
        bossHpSlider.gameObject.SetActive(true);
    }
}