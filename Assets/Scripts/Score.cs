using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score = 0;
    TMP_Text scoreText;
    public static int goalScore;

    [SerializeField] int goalScoreInput;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>(); // Text (TMP) 컴포넌트 잡기
        goalScore = goalScoreInput;
    }

    void Update()
    {
        if (score > goalScore)
        {
            scoreText.text = "BOSS!!!";
        }
        else
        {
            scoreText.text = score.ToString();
        }
    }

    public int getGoalScore()
    {
        return goalScore;
    }
}