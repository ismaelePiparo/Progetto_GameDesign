using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class CutScene : MonoBehaviour
{
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _cutScene1;
    [SerializeField] private VideoClip[] videos = new VideoClip[5];

    private VideoPlayer vp;
    private int _lenght;


    
    

    // Start is called before the first frame update
    void Start()
    {
        vp = _cutScene1.GetComponent<VideoPlayer>();
        StartCoroutine("StartCutScene", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(!ThirdPersonUnityCharacterController._playingFlute && KeySequence._isCorrect)
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene",2);
        }
        //vp.clip = videos[0];

        //vp.Play();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("esco");
            StopCoroutine("StartCutScene");
            _mainCam.SetActive(true);
            _cutScene1.SetActive(false);
            
        }
        //if (GuardSimple._isDied)
        //{
        //    Time.timeScale = 0;
        //    StartCoroutine("StartCutScene", 1);
        //}
    }

    IEnumerator StartCutScene(int a)
    {
        
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = videos[a];
        _lenght = (int) videos[a].length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght-0.5f);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
    }


    IEnumerator StartCutSene()
    {
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
    }
}
