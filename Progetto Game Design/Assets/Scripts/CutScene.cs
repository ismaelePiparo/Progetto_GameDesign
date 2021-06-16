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
    //[SerializeField] private VideoClip Tutorial_2;
    [SerializeField] private VideoClip EndScene;
    [SerializeField] private VideoPlayer Lampo;
    [SerializeField] private VideoPlayer Terremoto;
    [SerializeField] private VideoPlayer Vento;
   
    [SerializeField] private VideoPlayer MagicTree;



    [SerializeField] private string _nextScene;

    private VideoPlayer vp;
    private int _lenght,_op;
    private bool _skipFinal = false;

    private Scene currentScene;
    
    

    // Start is called before the first frame update
    void Start()
    {
        vp = _cutScene1.GetComponent<VideoPlayer>();
            
        currentScene = SceneManager.GetActiveScene();
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
        Debug.Log(currentScene.name);
        if (Input.GetKeyDown(KeyCode.Return) && !KeySequence._isCorrect)
        {
            if (_skipFinal)
            {

                //if (currentScene.name == "Tutorial_1")
                //{
                //    SceneManager.LoadScene("Tutorial_2");
                //}
                //else if (currentScene.name == "Tutorial_2")
                //{
                //    SceneManager.LoadScene("Accampamento");
                //}
                //else if (currentScene.name == "Accampamento")
                //{
                //    SceneManager.LoadScene("BossFinale");
                //}
                SceneManager.LoadScene(_nextScene);
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
            KeySequence._isCorrect = false;
            // StartCoroutine("StartCutScene", Lampo);
            // Lampo.SetActive(true);
            //Lampo.GetComponent<VideoPlayer>().Play();
            Lampo.Play();


        }
        if (string.Equals(i, "wind"))
        {
            KeySequence._isCorrect = false;
            Vento.Play();

        }
        if (string.Equals(i, "quake"))
        {
            KeySequence._isCorrect = false;
            Terremoto.Play();
        }
        if (string.Equals(i, "rise"))
        {
            KeySequence._isCorrect = false;
            MagicTree.Play();
        }
        if (string.Equals(i, "end"))
        {
            _skipFinal = true;
            StartCoroutine("StartEndScene", EndScene);
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
        _lenght = (int)a.length;
        vp.Play();
        yield return new WaitForSecondsRealtime(_lenght);
        KeySequence._isCorrect = false;
        Time.timeScale = 1;
        //if (currentScene.name == "Tutorial_1")
        //{
        //    SceneManager.LoadScene("Tutorial_2");
        //}
        //else if(currentScene.name == "Tutorial_2")
        //{
        //    SceneManager.LoadScene("Accampamento");
        //}
        //else if(currentScene.name == "Accampamento")
        //{
        //    SceneManager.LoadScene("BossFinale");
        //}
        SceneManager.LoadScene(_nextScene);
        
        _mainCam.SetActive(true);
        _cutScene1.SetActive(false);
        _canvas.SetActive(true);
    }

}
