using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    [SerializeField] private List<GameObject> _lives;
    [SerializeField] private GameObject _pentagram;
    [SerializeField] private GameObject _musicSheet;
    [SerializeField] private CutScene _cutScene;
    [SerializeField] private List<GameObject> _operai;
    //[SerializeField] private List<GameObject> _notes;


    private int i,n;
    private int _time=5;
    public static bool _decreaseLife = false;
    private string video;
    private int _opInScene;


    
    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        i = _lives.Count;
        //n = _notes.Count;
        if (_operai.Count != 0)
        {
            _opInScene = _operai.Count;
            Debug.Log(_opInScene + " operai in scena");

        }
        else
        {
            Debug.Log("no operai in scena");
        }
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

    }

    // Update is called once per frame
    void Update()
    {
        _decreaseLife = false;

        int _opSconfitti = _operai.Count(item => !item.activeSelf);
        //int count = _operai.Count(item => item.gameObject.GetComponent<GuardSimple>()._isDied);

        Debug.Log("Operai Sconfitti" + _opSconfitti);
        
        if(sceneName=="Tutorial_1" && _opSconfitti == _opInScene)
        {
            _cutScene.LaunchCutScene("end");
            return;
        }

        if (ThirdPersonUnityCharacterController._playFlute)
        {
            _time = 5;
            Time.timeScale = 0.5f;
            StartCoroutine("Note");
        }

        if (KeySequence._isCorrect && _pentagram.activeSelf)
        {
            StopCoroutine("Note");
            Time.timeScale = 1;
            video = KeySequence._mossa;
            _decreaseLife = true;
            _pentagram.SetActive(false);
        }

        // se il pentagramma è spento Spegne le note
        //if (!_pentagram.activeSelf)
        //{
        //    for (int j = 0; j < n; j++)
        //    {
        //        _notes[j].SetActive(false);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Tab) && !ThirdPersonUnityCharacterController._playingFlute && !_musicSheet.activeSelf)
        {
            Time.timeScale = 0;
            _musicSheet.SetActive(true);
        }else if (_musicSheet.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 1;
            _musicSheet.SetActive(false);
        }

        if (KeySequence._isCorrect && !ThirdPersonUnityCharacterController._playingFlute)
        {
            KeySequence._isCorrect = false;
            //Debug.Log("Sequenza Corretta; "+ KeySequence._mossa);
             _cutScene.LaunchCutScene(video);
        }

        

        


    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (i != 0 && !ChangeColor._safe)
            {
                _lives[i - 1].SetActive(false);
                i--;
            }
            else if (i==0) 
            {
                Debug.Log("sei morto!");
                Time.timeScale = 0;
            }
        }
    }

    public IEnumerator Note()
    {
        while (_time >= 0)
        {
            _time--;//Total time in seconds, countdown
            _pentagram.SetActive(true);
            if (_time == 0)
            {
                _pentagram.SetActive(false);
                Time.timeScale = 1;
                yield break;//Stop coroutine
            }
            else if (_time > 0)
            {

                yield return new WaitForSecondsRealtime(1);
            }
        }
    }
}
