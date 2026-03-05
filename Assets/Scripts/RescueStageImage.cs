using UnityEngine;
using UnityEngine.UI;

public class RescueStageImage : MonoBehaviour
{
    [SerializeField] private Image targetImage;

    // StageId(1,2,3...)에 대응되는 스프라이트들
    [SerializeField] private Sprite[] stageSprites;

    private void Awake()
    {
        if (targetImage == null)
            targetImage = GetComponent<Image>();
    }

    private void Start()
    {
        int stageId = PlayerPrefs.GetInt("StageId", 1); // 기본값 1
        SetStageImage(stageId);
    }

    private void SetStageImage(int stageId)
    {
        int index = stageId - 1; // stageId가 1부터라면
        if (stageSprites == null || stageSprites.Length == 0) return;

        if (index < 0) index = 0;
        if (index >= stageSprites.Length) index = stageSprites.Length - 1;

        targetImage.sprite = stageSprites[index];
    }
}