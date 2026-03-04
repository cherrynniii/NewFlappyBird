using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    private static BGMManager instance;
    private AudioSource audioSource;

    // BGM을 틀 씬 이름들 (여기에만 재생)
    private static readonly HashSet<string> allowedScenes = new HashSet<string>
    {
        "MainStartScene",
        "StageSelectScene"
    };

    void Awake()
    {
        // 중복 방지
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 시작 씬도 즉시 체크
        OnSceneLoaded(SceneManager.GetActiveScene(), LoadSceneMode.Single);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 허용된 씬이 아니면: 음악 끄고 자기 자신 삭제
        if (!allowedScenes.Contains(scene.name))
        {
            if (audioSource != null) audioSource.Stop();
            Destroy(gameObject);   // ✅ 다른 씬 갔다가 돌아오면 BGM이 새로 생겨서 "처음부터" 재생됨
            return;
        }

        // 허용된 씬이면 재생 (혹시 안 켜져 있으면 켜기)
        if (audioSource != null && !audioSource.isPlaying)
            audioSource.Play();
    }
}