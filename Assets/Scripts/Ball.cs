using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float Speed = 30.0f;
    public float secondsToStart = 2;
    public int Direction;
    public AudioSource WallSound;

    private Rigidbody2D rb;
    private float ForceX;
    private float ForceY=0.0f;
    private int[] RandomForces = new int[] { -1, 1 };
    private int RandomNum;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(WaitToStart(secondsToStart));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private float RaquetBounce(Vector2 BallPos, Vector2 PlayerPos, float PlayerHeight)
    {
        float Y = 0;
        float Range =( PlayerHeight / 3)/2;
        float RelativeY = (BallPos.y - PlayerPos.y/PlayerHeight);
        if (RelativeY <= Range && RelativeY>=Range)
            Y = 0;
        else if (RelativeY < -Range)
            Y = -1;
        else if (RelativeY > Range)
            Y=1;
        return Y;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            ForceX = -ForceX;
            ForceY = RaquetBounce(transform.position, collision.transform.position, collision.collider.bounds.size.y);
            rb.velocity = new Vector2(ForceX, ForceY).normalized * Speed;
        }
        else if (collision.gameObject.tag == "TB_wall")
        {
            WallSound.Play();
            if (rb.velocity.x == 0.0f)
            {
                RandomNum = Random.Range(0, RandomForces.Length);
                ForceX = RandomForces[RandomNum];
                rb.velocity = new Vector2(ForceX, ForceY).normalized * Speed;
            }
        }
        else if (collision.gameObject.tag == "RL_wall")
        {
            WallSound.Play();
            if (rb.velocity.y == 0.0f)
            {
                RandomNum = Random.Range(0, RandomForces.Length);
                ForceY = RandomForces[RandomNum];
                rb.velocity = new Vector2(ForceX, ForceY).normalized * Speed;
            }
        }
    }
    //Corrutina
    IEnumerator WaitToStart(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        switch (Direction)
        {
            case 0:
                RandomNum = Random.Range(0, RandomForces.Length);
                ForceX = RandomForces[RandomNum];
                break;
            case 1:
                ForceX = -1;
                break;
            case -1:
                ForceX = 1;
                break;
            default:
                RandomNum = Random.Range(0, RandomForces.Length);
                ForceX = RandomForces[RandomNum];
                break;
        }
        
        rb.velocity = new Vector2(ForceX, ForceY).normalized * Speed;
    }
}
