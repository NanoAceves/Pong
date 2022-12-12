using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject Player01_paddle;
    public GameObject Player02_paddle;
    [Space]
    public Text LeftScoreText;
    public Text RightScoreText;
    [Space]
    public GameObject P01_WinsSide;
    public GameObject P02_WinsSide;
    [Space]
    public Text P01_points;
    public Text P02_points;
    [Space]
    public GameObject ButtonsObj;


    private GameObject lastselected;
    // Start is called before the first frame update
    void Start()
    {

        P01_WinsSide.SetActive(false);
        P02_WinsSide.SetActive(false);
        //paddle positions
        float p01_posY = PlayerPrefs.GetFloat("P01_pos", -1000);
        float p02_posY = PlayerPrefs.GetFloat("P02_pos", -1000);

        if (p01_posY == -1000)
        {
            p01_posY = 0;
        }
        if (p02_posY == -1000)
        {
            p02_posY = 0;
        }

        Player01_paddle.transform.position = new Vector3(Player01_paddle.transform.position.x, p01_posY, Player01_paddle.transform.position.z);
        Player02_paddle.transform.position = new Vector3(Player02_paddle.transform.position.x, p02_posY, Player02_paddle.transform.position.z);

        //Player scores
        int WinnerScore = PlayerPrefs.GetInt("WinnerPoints", 0);
        int LoserScore = PlayerPrefs.GetInt("LoserPoints", 0);
        int difScore = PlayerPrefs.GetInt("ScoreDif", 0);
        string WinsPlayerId=PlayerPrefs.GetString("WinnerPlayer", "Player Unknown");

        if(WinsPlayerId=="Player 01")
        {
            LeftScoreText.text = WinnerScore.ToString();
            RightScoreText.text = LoserScore.ToString();
            P01_WinsSide.SetActive(true);
            P01_points.text = difScore.ToString();
            ButtonsObj.GetComponent<RectTransform>().position = new Vector3(P01_WinsSide.GetComponent<RectTransform>().position.x, ButtonsObj.GetComponent<RectTransform>().position.y, ButtonsObj.GetComponent<RectTransform>().position.z);
        }
        else
        {
            LeftScoreText.text = LoserScore.ToString();
            RightScoreText.text = WinnerScore.ToString();
            P02_WinsSide.SetActive(true);
            P02_points.text = difScore.ToString();
            ButtonsObj.GetComponent<RectTransform>().position = new Vector3(P02_WinsSide.GetComponent<RectTransform>().position.x, ButtonsObj.GetComponent<RectTransform>().position.y, ButtonsObj.GetComponent<RectTransform>().position.z);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            EventSystem.current.SetSelectedGameObject(lastselected);
        }
        else
        {
            lastselected = EventSystem.current.currentSelectedGameObject;
        }
    }

    public void PlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void MainMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
