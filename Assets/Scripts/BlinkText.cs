using UnityEngine;
using TMPro;

public class BlinkTMP : MonoBehaviour
{
    private TMP_Text text;
    public float speed = 2f;

    void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        Color c = text.color;
        c.a = Mathf.PingPong(Time.time * speed, 1f);
        text.color = c;
    }
}