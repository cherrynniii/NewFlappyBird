using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    private int stageId;

    // 게임 실행 후 인스턴스 초기화 (Start보다 한 단계 더 앞에서 수행)
    void Awake()
    {
#if UNITY_EDITOR
        // 테스트용: 플레이 버튼 누를 때마다 기록 초기화
        PlayerPrefs.DeleteKey("ClearedStage");
        PlayerPrefs.Save();
#endif

        if (instance == null)
        {
            instance = this;
        }

        // 스테이지 레벨 받기
        stageId = PlayerPrefs.GetInt("StageId", 1);
    }

    public int GetStageId()
    {
        return stageId;
    }
}
