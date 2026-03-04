using UnityEngine;
using UnityEngine.SceneManagement;

public class Rescue : MonoBehaviour
{
    public void RescueFriend()
    {
        SceneManager.LoadScene("RescueScene");
    }
}
