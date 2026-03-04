using UnityEngine;

public class MakeObstacle : MonoBehaviour
{
    public GameObject[] obstacles;
    float timer = 0.0f;
    public float timeDiff;

    // Update is called once per frame
    void Update()
    {
        if (Score.score >= Score.goalScore)
        {
            GameObject[] obs = GameObject.FindGameObjectsWithTag("Obstacle");
            foreach (GameObject o in obs)
            {
                Destroy(o);
            }
            return;
        }

        timer += Time.deltaTime;
        if (timer > timeDiff)
        {
            int index = Random.Range(0, obstacles.Length);
            GameObject newObstacle = Instantiate(obstacles[index]);
            if (index == 0)
            {
                newObstacle.transform.position = new Vector3(0.56f, Random.Range(-3.94f, 0.88f), 0);
            }
            else if (index == 1)
            {
                newObstacle.transform.position = new Vector3(2.71f, Random.Range(1.46f, 5.06f), 0);
            }
            else
            {
                newObstacle.transform.position = new Vector3(-3.96f, Random.Range(0.5f, 4.37f), 0);
            }
            timer = 0.0f;
            Destroy(newObstacle, 6.0f);
        }
    }
}