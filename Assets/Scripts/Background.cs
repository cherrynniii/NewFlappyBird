using UnityEngine;

public class Background : MonoBehaviour
{
    private float moveSpeed = 1.5f;

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        if (transform.position.x < -10.77f)
        {
            transform.position += new Vector3(30.58f, 0, 0);
        }
    }
}
