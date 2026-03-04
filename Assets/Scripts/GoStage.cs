using UnityEngine;
using UnityEngine.SceneManagement;

public class GoStage : MonoBehaviour
{
    public void Stage()
    {
        SceneManager.LoadScene("StageSelectScene");
    }
}
