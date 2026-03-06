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

    private int life = 3;
    public event System.Action<int> OnLifeChanged;

    [SerializeField] private GameObject weapon;
    [SerializeField] private Transform shootTransform;
    [SerializeField] private float shootInterval;    // 미사일 쏘는 주기
    private float lastShootTime;    // 마지막으로 미사일을 쏜 시간

    public AudioSource audioSource;
    public AudioClip jumpSound;
    public AudioClip bubbleSound;
    public AudioClip electricSound;
    public AudioClip damageSound;

    public Button waterGunButton;

    private bool invincible = false; // 무적 상태 여부
    private float invincibleTime = 0.8f;
    private Coroutine invincibleCo;

    private CameraManager camera;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        camera = FindFirstObjectByType<CameraManager>();
        life = 3;
        OnLifeChanged?.Invoke(life);
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
        if (other.gameObject.CompareTag("RealObstacle"))
        {
            audioSource.PlayOneShot(damageSound);
            TakeDamage();
            camera.ShakeCamera();
        }
        else if (other.gameObject.CompareTag("SharkWeapon"))
        {
            audioSource.PlayOneShot(damageSound);
            TakeDamage();
            camera.ShakeCamera();
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

    private void TakeDamage()
    {
        if (invincible) return;   // 무적이면 무시

        life--;
        OnLifeChanged?.Invoke(life);

        if (life <= 0)
        {
            SceneManager.LoadScene("GameOverScene");
            return;
        }

        // 무적 시작
        if (invincibleCo != null) StopCoroutine(invincibleCo);
        invincibleCo = StartCoroutine(InvincibleCoroutine(invincibleTime));
    }

    private IEnumerator InvincibleCoroutine(float t)
    {
        invincible = true;

        // (선택) 무적 표시: 살짝 투명하게
        Color c = sr.color;
        sr.color = new Color(c.r, c.g, c.b, 0.6f);

        yield return new WaitForSeconds(t);

        // 원상복구
        c = sr.color;
        sr.color = new Color(c.r, c.g, c.b, 1f);

        invincible = false;
    }

    public int GetLife()
    {
        return life;
    }
}
