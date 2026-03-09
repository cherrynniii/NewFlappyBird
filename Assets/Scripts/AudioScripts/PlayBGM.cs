using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayBGM : MonoBehaviour
{
    [Header("Clips")]
    [SerializeField] private AudioClip playClip;   // 일반 플레이 BGM
    [SerializeField] private AudioClip bossClip;   // 보스 BGM

    [Header("Volume")]
    [Range(0f, 1f)][SerializeField] private float playVolume = 0.35f;
    [Range(0f, 1f)][SerializeField] private float bossVolume = 0.45f;

    private AudioSource audioSource;
    private string playSceneName;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            return;
        }

        // 2D 권장
        audioSource.spatialBlend = 0f;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        playSceneName = SceneManager.GetActiveScene().name;
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
        // PlayScene 시작 시 플레이 BGM 재생
        PlayNormal();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // PlayScene을 벗어나면 이 오브젝트 제거 → 다른 씬에서는 재생 안 됨
        if (scene.name != playSceneName)
        {
            Destroy(gameObject);
        }
    }

    public void PlayNormal()
    {
        if (audioSource == null || playClip == null) return;

        audioSource.Stop();
        audioSource.clip = playClip;
        audioSource.volume = playVolume;
        audioSource.Play();
    }

    public void PlayBoss()
    {
        if (audioSource == null || bossClip == null) return;

        audioSource.Stop();
        audioSource.clip = bossClip;
        audioSource.volume = bossVolume;
        audioSource.Play();
    }
}