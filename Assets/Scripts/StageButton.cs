using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public int stageId;

    public Image icon;
    public Button button;
    public Sprite lockSprite;

    private Sprite originalSprite;

    void Start()
    {
        originalSprite = icon.sprite;

        int cleared = PlayerPrefs.GetInt("ClearedStage", 0);

        if (stageId <= cleared)
        {
            // 클리어한 스테이지 → 원래 색 그대로
            icon.sprite = originalSprite;
            button.interactable = true;
        }
        else if (stageId == cleared + 1)
        {
            // 열려있지만 아직 클리어 안함 → 회색
            icon.sprite = originalSprite;
            icon.color = icon.color = new Color(0.6f, 0.6f, 0.6f, 1f); ;
            button.interactable = true;
        }
        else
        {
            // 잠김 → 자물쇠
            icon.sprite = lockSprite;
            icon.color = icon.color = new Color(0.35f, 0.35f, 0.35f, 1f); ;
            button.interactable = false;
        }
    }
}