using UnityEngine;
using UnityEngine.SceneManagement;

public class Turtle : MonoBehaviour
{
    Rigidbody2D rb;
    public float jumpForce;

    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float shootInterval;    // 미사일 쏘는 주기
    private float lastShootTime;    // 마지막으로 미사일을 쏜 시간

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            rb.linearVelocity = Vector2.up * jumpForce;
        }

        if (Score.score >= Score.goalScore)
        {
            Shoot();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("RealObstacle")) 
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (other.gameObject.CompareTag("SharkWeapon"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
    }

    void Shoot()
    {
        if (Time.time - lastShootTime > shootInterval)
        {
            Instantiate(weapon, shootTransform.position, Quaternion.identity);
            lastShootTime = Time.time;
        }
    }
}
