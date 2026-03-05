using UnityEngine;
using UnityEngine.UI;

public class WaterGunAttackSpawner : MonoBehaviour
{
    [SerializeField] private GameObject waterGunAttackPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Button button;

    public void SpawnWaterGun()
    {
        button.gameObject.SetActive(false);
        GameObject bullet = Instantiate(waterGunAttackPrefab, spawnPoint.position, Quaternion.identity);
        Destroy(bullet, 1f);
    }
}
