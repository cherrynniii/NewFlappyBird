using UnityEngine;
using UnityEngine.UI;

public class BossHPBar : MonoBehaviour
{
    public Slider hpBar;
    private Shark boss;

    void Update()
    {
        if (boss == null)
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Shark");
            if (obj != null)
            {
                boss = obj.GetComponent<Shark>();
                hpBar.maxValue = boss.GetHP();
            }
        }

        if (boss != null)
        {
            hpBar.value = boss.GetHP();
        }
    }
}