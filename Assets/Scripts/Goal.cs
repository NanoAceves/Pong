using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Goal : MonoBehaviour
{
    public Text PlayerScore;
    public GameObject PrefabBall;
    public int Direction;
    public AudioSource GoalSound;
    [Space]
    public Transform player01_pos;
    public Transform player02_pos;
    [Space]
    public string PlayerGoalID;//id de jugador con portería ganadora
    public GameObject OtherGoal;

    private const int WinScore = 3; //15
    private int scoreDif;

    private int score;
    private GameObject NewBall;
    private float lastSpeed;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        PlayerScore.text = score.ToString();
        lastSpeed = PrefabBall.GetComponent<Ball>().Speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (score==WinScore)
        {
            scoreDif = WinScore - OtherGoal.GetComponent<Goal>().GetScore();

            PlayerPrefs.SetString("WinnerPlayer", PlayerGoalID);
            PlayerPrefs.SetInt("ScoreDif", scoreDif);

            PlayerPrefs.SetInt("WinnerPoints", WinScore);
            PlayerPrefs.SetInt("LoserPoints", OtherGoal.GetComponent<Goal>().GetScore());

            PlayerPrefs.SetFloat("P01_pos", player01_pos.position.y);
            PlayerPrefs.SetFloat("P01_pos", player02_pos.position.y);

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            GoalSound.Play();
            lastSpeed = collision.gameObject.GetComponent<Ball>().Speed;
            Destroy(collision.gameObject);
            score++;
            PlayerScore.text = score.ToString();

            NewBall = Instantiate(PrefabBall);
            NewBall.GetComponent<Ball>().Direction = Direction;
            lastSpeed = lastSpeed + 0.5f;
            NewBall.GetComponent<Ball>().Speed = lastSpeed;
        }
    }
    public int GetScore()
    {
        return score;
    }
}
