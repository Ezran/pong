using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Ball : MonoBehaviour
{
    public float speed = 30;
    public int paddleMultiplier = 20;
    public GameObject leftScoreboard;
    public GameObject rightScoreboard;
    private int lScore = 0;
    private int rScore = 0;
    private Vector2 INIT_POS;

    void Start()
    {
        if (leftScoreboard == null || rightScoreboard == null)
            return;
        INIT_POS = transform.position;
        // Initial Velocity
        GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        string objName = col.gameObject.name;
        if (objName == "RacketLeft" || objName == "RacketRight")
        {
            float y = paddleMultiplier * hitFactor(transform.position,
                                col.transform.position,
                                col.GetComponent<Collider2D>().bounds.size.y);

            Vector2 dir = new Vector2(GetComponent<Rigidbody2D>().velocity.x * -1, y).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().velocity = dir * speed;
        }
        else if (objName == "WallTop" || objName == "WallBottom")
        {
            Vector2 v = GetComponent<Rigidbody2D>().velocity;
            GetComponent<Rigidbody2D>().velocity = new Vector2(v.x, v.y * -1);
        }
        else if (objName == "WallLeft")
        {
            rScore++;
            rightScoreboard.GetComponent<Text>().text = rScore.ToString();
            transform.position = INIT_POS;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;

        }
        else if (objName == "WallRight")
        {
            lScore++;
            leftScoreboard.GetComponent<Text>().text = lScore.ToString();
            transform.position = INIT_POS;
            GetComponent<Rigidbody2D>().velocity = Vector2.right * speed;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos,
                float racketHeight)
    {
        return (ballPos.y - racketPos.y) / racketHeight;
    }
}