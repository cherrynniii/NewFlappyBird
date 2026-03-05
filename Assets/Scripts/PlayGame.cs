using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public void Play(int stageId)
    {
        // PlayerPrefs: Unity가 제공하는 간단한 저장 시스템
        PlayerPrefs.SetInt("StageId", stageId);
        PlayerPrefs.Save();
        SceneManager.LoadScene("PlayScene");
    }
}
