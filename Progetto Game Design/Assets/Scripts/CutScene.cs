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

    //[SerializeField] private  List<GameObject> _operai;

    [SerializeField] private VideoClip Tutorial_1;
    [SerializeField] private VideoClip Tutorial_2;
    [SerializeField] private VideoClip Tutorial_3;
    [SerializeField] private VideoClip Lampo;
    [SerializeField] private VideoClip Terremoto;
    [SerializeField] private VideoClip Vento;
    [SerializeField] private VideoClip Albero;

    private VideoPlayer vp;
    private int _lenght,_op;
    private bool _skipFinal = false;


    
    

    // Start is called before the first frame update
    void Start()
    {
        vp = _cutScene1.GetComponent<VideoPlayer>();
            
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Time.timeScale = 1;
        if (sceneName == "Tutorial_1")
        {
            StartCoroutine("StartCutScene", Tutorial_1);
        }
        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Return) && !KeySequence._isCorrect)
        {
            if (_skipFinal)
            {
                SceneManager.LoadScene("Tutorial_2");
            }
            else
            {
                Time.timeScale = 1;
                StopCoroutine("StartCutScene");
                _mainCam.SetActive(true);
                _cutScene1.SetActive(false);
                _canvas.SetActive(true);
            }

        }
    }

    public void LaunchCutScene(string i)
    {
        
        if (string.Equals(i, "flash"))
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene", Lampo);
        }
        if (string.Equals(i, "wind"))
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene", Vento);
        }
        if (string.Equals(i, "quake"))
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene", Terremoto);
        }
        if (string.Equals(i, "rise"))
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene", Albero);
        }
        if (string.Equals(i, "end"))
        {
            Time.timeScale = 0;
            _skipFinal = true;
            StartCoroutine("StartEndScene", Tutorial_2);
        }
    }


    IEnumerator StartCutScene(VideoClip a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = a;
        _lenght = (int) a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
        _canvas.SetActive(true);
    }

    IEnumerator StartEndScene(VideoClip a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = a;
        _lenght = (int)a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial_2");
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
        _canvas.SetActive(true);
    }

}
