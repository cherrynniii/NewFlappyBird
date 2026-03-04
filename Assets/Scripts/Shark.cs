using UnityEngine;

public class Shark : MonoBehaviour
{
    public float targetX = 2.72f;
    public float speed = 2f;

    void Update()
    {
        Vector3 target = new Vector3(targetX, transform.position.y, transform.position.z);

        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            speed * Time.deltaTime
        );
    }
}