using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Play_Button_Script : MonoBehaviour
{

    [SerializeField] GameObject VideoPlayer;
    [SerializeField] GameObject VideoRawImage;


    // Start is called before the first frame update
    void Start()
    {
        VideoPlayer.SetActive(false);
        VideoRawImage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayButton()
    {
        VideoPlayer.SetActive(true);
        VideoRawImage.SetActive(true);

    }

    public void SkipButton()
    {
        SceneManager.LoadScene("Tutorial_1");
    }
}
