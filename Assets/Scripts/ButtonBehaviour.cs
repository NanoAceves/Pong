using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonBehaviour : MonoBehaviour
{
    public GameObject ImageBox;
    public Text ButtonText;
    public AudioSource OutSound;
    public AudioSource SelectSound;

    // Start is called before the first frame update
    void Start()
    {
        ImageBox.SetActive(false);
        ButtonText.fontStyle = FontStyle.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSelectedObj()
    {
        ImageBox.SetActive(true);
        ButtonText.fontStyle = FontStyle.Bold;
    }

    public void OnDeselectedObj()
    {
        ImageBox.SetActive(false);
        ButtonText.fontStyle = FontStyle.Normal;
        OutSound.Play();
    }

    public void onEnterObj()
    {
        SelectSound.Play();
    }
}
