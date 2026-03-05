using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static int score;
    TMP_Text scoreText;
    public static int goalScore;

    [SerializeField] int goalScoreInput;

    void Awake()
    {
        scoreText = GetComponent<TMP_Text>();
        goalScore = goalScoreInput;
    }

    void Start()
    {
        score = 0;
        if (GameManager.instance.GetStageId() == 1)
        {
            goalScore = 10;
        }
        else if (GameManager.instance.GetStageId() == 2)
        {
            goalScore = 20;
        }
    }

    void Update()
    {
        if (score >= goalScore)
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