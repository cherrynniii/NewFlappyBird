using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    TMP_Text scoreText;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>(); // Text (TMP) 컴포넌트 잡기
    }

    void Update()
    {
        scoreText.text = score.ToString();
    }
}