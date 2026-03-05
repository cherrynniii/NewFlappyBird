using UnityEngine;
using UnityEngine.UI;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private Turtle turtle;
    [SerializeField] private Image[] hearts;

    [SerializeField] private Sprite fullHeart;   // 빨간 하트
    [SerializeField] private Sprite emptyHeart;  // 회색 하트

    void Start()
    {
        turtle.OnLifeChanged += UpdateLifeUI;
        UpdateLifeUI(turtle.GetLife()); // 초기 UI 세팅
    }

    void OnDestroy()
    {
        turtle.OnLifeChanged -= UpdateLifeUI;
    }

    void UpdateLifeUI(int life)
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < life)
                hearts[i].sprite = fullHeart;
            else
                hearts[i].sprite = emptyHeart;
        }
    }
}