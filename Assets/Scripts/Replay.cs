using UnityEngine;
using UnityEngine.SceneManagement;

public class Replay : MonoBehaviour
{
    public void ReplayGame()
    {
        SceneManager.LoadScene("PlayScene");
    }
}
