using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;


public class Play_Button_Script : MonoBehaviour
{

    [SerializeField] GameObject vp;
    [SerializeField] GameObject VideoRawImage;


    // Start is called before the first frame update
    void Start()
    {
        vp.SetActive(false);
        VideoRawImage.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (vp.activeSelf && vp.GetComponent<VideoPlayer>().isPaused) {
            SceneManager.LoadScene("Tutorial_1");
        }
    }

    public void PlayButton()
    {
        vp.SetActive(true);
        VideoRawImage.SetActive(true);

    }

    public void SkipButton()
    {
        SceneManager.LoadScene("Tutorial_1");
    }
}
