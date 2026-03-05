using UnityEngine;
using UnityEngine.SceneManagement;

public class Shark : MonoBehaviour
{
    public float targetX = 2.72f;
    public float speed = 2f;

    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform shootTransform;
    private float shootInterval;    // 미사일 쏘는 주기
    private float lastShootTime;    // 마지막으로 미사일을 쏜 시간

    // 상어가 위 아래로 움직일 수 있는 범위
    private float minY = -2.55f;
    private float maxY = 3.88f;

    // 상어 맞을 때 나는 소리
    public AudioSource audioSource;
    public AudioClip hitSound;

    private bool isEntering = true;

    private bool canHit = false; // 상어가 처음 등장할 때는 맞지 않도록 설정

    private int hp;

    Vector3 target;

    private void Start()
    {
        SetNewTarget();
        int stageId = GameManager.instance.GetStageId();

        if (stageId == 1)
        {
            hp = 15;
            shootInterval = 2f;
        }
        else if (stageId == 2)
        {
            hp = 30;
            shootInterval = 1.5f;
        }
    }

    void Update()
    {
        // 처음 x좌표로 이동한 후, y좌표는 랜덤하게 움직이도록 설정
        if (isEntering)
        {
            Vector3 enterTarget = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.MoveTowards(
                transform.position,
                enterTarget,
                speed * Time.deltaTime
            );

            if (Mathf.Abs(transform.position.x - targetX) < 0.05f)
            {
                isEntering = false;
                canHit = true; // 상어가 처음 등장한 후에 맞을 수 있도록 설정
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                speed * Time.deltaTime
            );

            if (Vector3.Distance(transform.position, target) < 0.1f)
            {
                SetNewTarget();
            }
        }

        Shoot();
    }

    void Shoot()
    {
        if (Time.time - lastShootTime > shootInterval)
        {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShootTime = Time.time;
        }
    }

    void SetNewTarget()
    {
        float randomY = Random.Range(minY, maxY);
        target = new Vector3(targetX, randomY, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!canHit) return; // 상어가 처음 등장할 때는 맞지 않도록 설정
        if (other.gameObject.CompareTag("Bubble"))
        {
            audioSource.PlayOneShot(hitSound, 0.6f);
            Bubble bubble = other.gameObject.GetComponent<Bubble>();
            hp -= bubble.damage;
            Destroy(other.gameObject);  // 버블은 항상 닿으면 사라지게
        }
        else if (other.gameObject.CompareTag("WaterGunShoot"))
        {
            audioSource.PlayOneShot(hitSound, 0.6f);
            hp -= 3;

        }

        if (hp <= 0)
        {
            int stageId = GameManager.instance.GetStageId();
            int cleared = PlayerPrefs.GetInt("ClearedStage", 0);

            if (stageId > cleared)
            {
                PlayerPrefs.SetInt("ClearedStage", stageId);
                PlayerPrefs.Save();
            }

            Destroy(gameObject);
            SceneManager.LoadScene("GameClearScene");
        }
    }

    public int GetHP()
    {
        return hp;
    }
}