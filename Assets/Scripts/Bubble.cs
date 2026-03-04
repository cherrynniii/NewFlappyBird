using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    public int damage = 1;


    private void Start()
    {
        Destroy(gameObject, 3f); // 5초 후에 버블 오브젝트를 제거
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}