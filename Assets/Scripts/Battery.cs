using UnityEngine;

public class Battery : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
