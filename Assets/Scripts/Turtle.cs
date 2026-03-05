using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Turtle : MonoBehaviour
{
    Rigidbody2D rb;
    SpriteRenderer sr;  // 거북이 색 변화
    private float jumpForce = 3.5f;

    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float shootInterval;    // 미사일 쏘는 주기
    private float lastShootTime;    // 마지막으로 미사일을 쏜 시간

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip bubbleSound;
    public AudioClip electricSound;

    public Button waterGunButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            rb.linearVelocity = Vector2.up * jumpForce;
            audioSource.PlayOneShot(jumpSound);
        }

        if (Score.score >= Score.goalScore)
        {
            Shoot();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SharkWeapon"))
        {
            SceneManager.LoadScene("GameOverScene");
        }
        else if (other.gameObject.CompareTag("Battery"))
        {
            Destroy(other.gameObject); // 배터리 먹기
            audioSource.PlayOneShot(electricSound, 2f);
            StartCoroutine(JumpDebuff());
        }
        else if (other.gameObject.CompareTag("WaterGun"))
        {
            waterGunButton.gameObject.SetActive(true);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("RealObstacle"))
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
            audioSource.PlayOneShot(bubbleSound, 0.6f);
        }
    }

    IEnumerator JumpDebuff()
    {
        jumpForce = 2.5f;

        // 서서히 어두워짐
        yield return StartCoroutine(FadeColor(new Color(0.6f, 0.6f, 0.6f, 1f), 0.4f));

        yield return new WaitForSeconds(3f);

        jumpForce = 3.5f;

        // 서서히 밝아짐
        yield return StartCoroutine(FadeColor(Color.white, 0.4f));
    }

    IEnumerator FadeColor(Color targetColor, float duration)
    {
        Color startColor = sr.color;
        float time = 0f;

        while (time < duration)
        {
            sr.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        sr.color = targetColor;
    }
}
