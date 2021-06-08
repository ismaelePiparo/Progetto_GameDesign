using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using System.Linq;
using UnityEngine.SceneManagement;

public class CutScene : MonoBehaviour
{
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _cutScene1;
    [SerializeField] private GameObject _canvas;
    [SerializeField] private VideoClip[] videos = new VideoClip[5];

    [SerializeField] private  List<GameObject> _operai;

    [SerializeField] private VideoClip Tutorial_1;
    [SerializeField] private VideoClip Tutorial_2;
    [SerializeField] private VideoClip Tutorial_3;
    [SerializeField] private VideoClip Lampo;
    [SerializeField] private VideoClip Terremoto;
    [SerializeField] private VideoClip Vento;
    [SerializeField] private VideoClip Albero;

    private VideoPlayer vp;
    private int _lenght,_op;


    
    

    // Start is called before the first frame update
    void Start()
    {
        vp = _cutScene1.GetComponent<VideoPlayer>();
        if (_operai.Count !=0)
        {
            _op = _operai.Count;

        }
        else
        {
            Debug.Log("no operai in scena");
        }
       
        
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Tutorial_1")
        {
            StartCoroutine("StartCutScene", 0);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (_op != 0)
        {
            int count = _operai.Count(item => item.gameObject.GetComponent<GuardSimple>()._isDied);
            int attivi = _operai.Count(item => !item.activeSelf);


            if (!ThirdPersonUnityCharacterController._playingFlute && KeySequence._isCorrect)
            {
                Time.timeScale = 0;
                StartCoroutine("StartCutScene", 2);
            }
            
            if (Input.GetKeyDown(KeyCode.Return) && !KeySequence._isCorrect)
            {
                if (count == _op)
                {
                    Debug.Log("Skippo il finale");
                    SceneManager.LoadScene("Tutorial_2");

                }
                else
                {
                    StopCoroutine("StartCutScene");
                    _mainCam.SetActive(true);
                    _cutScene1.SetActive(false);
                    _canvas.SetActive(true);
                }

            }




            if (attivi == _op)
            {
                
                Debug.Log("tutti gli operai sono morti!");
                StartCoroutine("StartEndScene", 1);
            }

        }
    }

    IEnumerator StartCutScene(int a)
    {
        _canvas.SetActive(false);
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
        _canvas.SetActive(true);
    }

    IEnumerator StartEndScene(int a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = videos[a];
        _lenght = (int)videos[a].length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght+10);
        KeySequence._isCorrect = false;
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
        _canvas.SetActive(true);
        SceneManager.LoadScene("Tutorial_2");

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
