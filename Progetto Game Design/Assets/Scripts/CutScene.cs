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

    [SerializeField] private VideoClip IntroCutScene;
    //[SerializeField] private VideoClip Tutorial_2;
    [SerializeField] private VideoClip MissionCompletedCutScene;
    [SerializeField] private VideoClip FailedCutScene;
    [SerializeField] private VideoPlayer Lampo;
    [SerializeField] private VideoPlayer Terremoto;
    [SerializeField] private VideoPlayer Vento;
   
    [SerializeField] private VideoPlayer MagicTree;



    [SerializeField] public string _nextScene;
    [SerializeField] public string _previusScene;

    private VideoPlayer vp;
    private float _lenght,_op;
    private bool _skipFinal = false;
    private bool _skipIntro = true;

    private Scene currentScene;


    private void Awake()
    {
        if (IntroCutScene != null)
        {
            _skipIntro = true;
        }
        else
        {
            _skipIntro = false;
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        vp = _cutScene1.GetComponent<VideoPlayer>();
            
        currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        Time.timeScale = 1;
        if (sceneName == "Tutorial_1")
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutScene", IntroCutScene);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(currentScene.name);
        if (Input.GetKeyDown(KeyCode.Return) && !KeySequence._isCorrect)
        {
            if (_skipFinal)
            {

                SceneManager.LoadScene(_nextScene);
            }
            else if(_skipIntro)
            {
                _skipIntro = false;
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
            KeySequence._isCorrect = false;
            // StartCoroutine("StartCutScene", Lampo);
            // Lampo.SetActive(true);
            //Lampo.GetComponent<VideoPlayer>().Play();
            Lampo.Play();
            Lampo.GetComponent<AudioSource>().Play();


        }
        if (string.Equals(i, "wind"))
        {
            KeySequence._isCorrect = false;
            Vento.Play();
            Vento.GetComponent<AudioSource>().Play();

        }
        if (string.Equals(i, "quake"))
        {
            KeySequence._isCorrect = false;
            Terremoto.Play();
            Terremoto.GetComponent<AudioSource>().Play();
        }
        if (string.Equals(i, "rise"))
        {
            KeySequence._isCorrect = false;
            MagicTree.Play();
            MagicTree.GetComponent<AudioSource>().Play();
        }
        if (string.Equals(i, "end"))
        {

            if(currentScene.name=="Tutorial_1" || currentScene.name == "Tutorial_2")
            {
                _skipFinal = true;

            }
            else
            {
                _skipFinal = false;
            }
            

            StartCoroutine("StartEndScene", MissionCompletedCutScene);
        }
        if (string.Equals(i, "failed"))
        {
            _skipFinal = false;
            StartCoroutine("ReturnPreviusScene", FailedCutScene);
        }
    }


    IEnumerator StartCutScene(VideoClip a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = a;
        _lenght = (float) a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
        _canvas.SetActive(true);
    }


    IEnumerator Reset(GameObject a)
    {
        yield return new WaitForSecondsRealtime(5f);
        a.SetActive(false);
    }

    IEnumerator StartEndScene(VideoClip a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = a;
        _lenght = (float) a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
       
        SceneManager.LoadScene(_nextScene);
        
        //_mainCam.SetActive(true);
        //_cutScene1.SetActive(false);
        //_canvas.SetActive(true);
    }

    IEnumerator ReturnPreviusScene(VideoClip a)
    {
        _canvas.SetActive(false);
        _mainCam.SetActive(false);
        _cutScene1.SetActive(true);
        vp.clip = a;
        _lenght = (float)a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;

        SceneManager.LoadScene(_previusScene);

        //_mainCam.SetActive(true);
        //_cutScene1.SetActive(false);
        //_canvas.SetActive(true);
    }

}
