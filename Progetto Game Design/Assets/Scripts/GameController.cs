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
    //[SerializeField] private List<GameObject> _operai;
    //[SerializeField] private List<GameObject> _tree;
    //[SerializeField] private int tempoDiGioco=0;
    [SerializeField] private GameObject MascheraAlbero;
    //[SerializeField] private List<GameObject> _notes;
    [SerializeField] private List<GameObject> _missions;

    private int i;
    private int _time=5;
    public static bool _decreaseLife = false;
    public static bool _alberoCurato = false;
    private string video;
    private int _opInScene;
    private int _treeInScene;

    private int _currentMission;

    public static bool _colpita = false;

    private string sceneName;

    // Start is called before the first frame update
    void Start()
    {
        i = _lives.Count;

        InvokeRepeating("ChangePosition", 0,2);



        //if (_operai.Count != 0)
        //{
        //    _opInScene = _operai.Count;
        //    Debug.Log(_opInScene + " operai in scena");

        //}
        //else
        //{
        //    Debug.Log("No operai in scena");
        //}
        //if (_tree.Count != 0)
        //{
        //    _treeInScene = _tree.Count;
        //    Debug.Log(_treeInScene + " alberi in scena");

        //}
        //else
        //{
        //    Debug.Log("No alberi in scena");
        //}
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentMission = ThirdPersonUnityCharacterController._IDtarget;
        _decreaseLife = false;
        _alberoCurato = false;

        //int _opSconfitti = _operai.Count(item => !item.activeSelf);
        //int _alberiCurati = _tree.Count(item => item.gameObject.GetComponent<TreeRise>()._foglieAttive);

        int _missionCompleted = _missions.Count(item => item.GetComponent<MissionID>()._completed);
        Debug.Log("NUMBER OF MISSIONS = " + _missions.Count + ", MISSION COMPLETED = " + _missionCompleted + ", CURRENT MISSION = "+ _currentMission);

        //SE TUTTI GLI OPERAI SONO STATI SCONFITTI O GLI ALBERI CURATI LANCIA LA CUTSCENE FINALE
        //if(sceneName=="Tutorial_1" && _opSconfitti == _opInScene)
        //{
        //    _cutScene.LaunchCutScene("end");
        //    return;
        //}
        //if (sceneName == "Tutorial_2" && _alberiCurati == _treeInScene)
        //{
        //    _cutScene.LaunchCutScene("end");
        //    return;
        //}

        //if (_opSconfitti == _opInScene)
        //{
        //    if (_alberiCurati == _treeInScene)
        //    {
        //        _cutScene.LaunchCutScene("end");
        //        return;
        //    }
        //    else
        //    {
        //        if (_treeInScene == 0)
        //        {
        //            _cutScene.LaunchCutScene("end");
        //            return;
        //        }
        //    }
        //}
        //else if(_opInScene==0)
        //{
        //    if (_alberiCurati == _treeInScene)
        //    {
        //        _cutScene.LaunchCutScene("end");
        //        return;
        //    }
        //}

        if (_missions.Count == _missionCompleted)
        {
            _cutScene.LaunchCutScene("end");
            return;
        }


        //SE PAN SUONA IL FLAUTO LANCIA LA CORUTINE DEL PENTAGRAMMA
        if (ThirdPersonUnityCharacterController._playFlute)
        {
            _time = 5;
            Time.timeScale = 0.5f;
            StartCoroutine("Note");
        }

        //SE LA SEQUENZA é CORRETTA SPEGNE IL PENTAGRAMMA DECREMENTA LA VITA/CURA L'ALBERO E IMPOSTA LA CUTSCENE DA LANCIARE
        if (KeySequence._isCorrect && _pentagram.activeSelf)
        {
            StopCoroutine("Note");
            Time.timeScale = 1;
            
            //if (_operai.Count != 0)
            //{
            //    _decreaseLife = true;
            //}
            //else if(_tree.Count!=0)
            //{
            //    Debug.Log("hai curato l'albero!");
            //    _alberoCurato = true;
            //}
            //_pentagram.SetActive(false);

            if (!_missions[_currentMission-1].GetComponent<MissionID>()._operaioSconfitto)
            {
                if (KeySequence._mossa == "rise")
                {
                    Debug.Log("MOSSA SBAGLIATA!!!");
                    video = "";
                }
                else
                {
                    video = KeySequence._mossa;
                    _decreaseLife = true;
                }
                
            }
            else if (!_missions[_currentMission - 1].GetComponent<MissionID>()._completed)
            {
                if (KeySequence._mossa == "rise")
                {
                    video = KeySequence._mossa;
                    _alberoCurato = true;
                }
                else
                {
                    Debug.Log("MOSSA SBAGLIATA!!!");
                    video = "";
                }
                
            }
            _pentagram.SetActive(false);
        }

        //SE LA SEQUENZA é CORRETT E PAN HA FINITO DI SUONARE LANCIA LA CUTSCENE
        if (KeySequence._isCorrect && !ThirdPersonUnityCharacterController._playingFlute)
        {
            KeySequence._isCorrect = false;
            //Debug.Log("Sequenza Corretta; "+ KeySequence._mossa);
            _cutScene.LaunchCutScene(video);
        }

        //GESTIONE DEL TAB
        if (Input.GetKeyDown(KeyCode.Tab) && !ThirdPersonUnityCharacterController._playingFlute && !_musicSheet.activeSelf)
        {
            Time.timeScale = 0;
            _musicSheet.SetActive(true);
        }else if (_musicSheet.activeSelf && Input.GetKeyDown(KeyCode.Tab))
        {
            Time.timeScale = 1;
            _musicSheet.SetActive(false);
        }


        //PAN é COLPITA => TOLGO VITE
        if (_colpita)
        {
            Colpita();
        }


    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Enemy"))
    //    {
    //        Debug.Log("colpita"+i);
    //        if (i != 0 && !ChangeColor._safe)
    //        {
    //            Debug.Log("vite rimaste" + i);
    //            _lives[i - 1].SetActive(false);
    //            i--;
    //        }
    //        else if (i==0) 
    //        {
    //            Debug.Log("sei morto!");
    //            Time.timeScale = 0;
    //        }
    //    }
    //}

    public IEnumerator Note()
    {

        yield return new WaitForSecondsRealtime(1);
        if (!ThirdPersonUnityCharacterController._playingFlute)
        {
            Time.timeScale = 1;
            yield break;//Stop coroutine
        }

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


    void ChangePosition()
    {
        
        //Compute position for next time
        Vector3 spwanPosition = new Vector3(0, 1, 0);
        MascheraAlbero.transform.localPosition = MascheraAlbero.transform.localPosition - spwanPosition;
        if (MascheraAlbero.transform.localPosition.y < 25) 
        {
            Time.timeScale = 0;
            //HAI PERSO!!!
        }
        
        
    }


    public void Colpita()
    {

        if (i != 0)
        {
            Debug.Log("vite rimaste" + i);
            _lives[i - 1].SetActive(false);
            i--;
        }
        else if (i == 0)
        {
            Debug.Log("sei morto!");
            Time.timeScale = 0;
        }
        _colpita = false;
    }


}
