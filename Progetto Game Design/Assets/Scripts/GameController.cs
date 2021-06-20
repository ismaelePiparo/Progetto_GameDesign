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
    [SerializeField] private GameObject MascheraAlbero;
    [SerializeField] private List<GameObject> _missions;

    [SerializeField] private GameObject _inutileSuonare;
    [SerializeField] private GameObject _troppoDistante;
    [SerializeField] private GameObject _melodiaSbagliata;

    [SerializeField] private GameObject _allertTime;

    private int i;
    private int _time=2;
    public static bool _decreaseLife = false;
    public static bool _alberoCurato = false;
    private string video;
   

    private int _currentMission;

    public static bool _colpita = false;

    private string sceneName;


    public static bool _attivoPentagramma = true;


    private int seconds = 0;

    // Start is called before the first frame update
    void Start()
    {
        i = _lives.Count;

        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;

        if (sceneName!= "ScenaEsplorazioneBosco_Lama" || sceneName != "ScenaEsplorazioneBosco_Vecchio" || sceneName != "ScenaEsplorazioneBosco_Witch") {
            InvokeRepeating("ChangePosition", 0, 2);
        }
        _allertTime.SetActive(false);
       

    }

    private void Awake()
    {
        _inutileSuonare.SetActive(false);
        _troppoDistante.SetActive(false);
        _melodiaSbagliata.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _currentMission = ThirdPersonUnityCharacterController._IDtarget;
        _decreaseLife = false;
        _alberoCurato = false;

        int _missionCompleted = _missions.Count(item => item.GetComponent<MissionID>()._completed);
        //Debug.Log("NUMBER OF MISSIONS = " + _missions.Count + ", MISSION COMPLETED = " + _missionCompleted + ", CURRENT MISSION = "+ _currentMission);


        if (_missions.Count == _missionCompleted && _missions.Count!=0)
        {
            _cutScene.LaunchCutScene("end");
            return;
        }



        //SE PAN SUONA IL FLAUTO LANCIA LA CORUTINE DEL PENTAGRAMMA
        if (ThirdPersonUnityCharacterController._playFlute)
        {
            if (_missions.Count == 0)
            {
                //Debug.Log("Non succede niente");
                StartCoroutine("InutileSuonare");
                video = "";
            }
            else if (!ThirdPersonUnityCharacterController._inCollider)
            {
                //Debug.Log("Troppo lontana");
                StartCoroutine("TroppoLontana");
                video = "";
            }
            else if (_missions[_currentMission - 1].GetComponent<MissionID>()._completed)
            {
                StartCoroutine("InutileSuonare");
                video = "";
            }
            else
            {
                _time = 5;
                Time.timeScale = 0.5f;
                StartCoroutine("Note");
            }
        }



        //SE LA SEQUENZA é CORRETTA SPEGNE IL PENTAGRAMMA DECREMENTA LA VITA/CURA L'ALBERO E IMPOSTA LA CUTSCENE DA LANCIARE
        if (KeySequence._isCorrect && _pentagram.activeSelf)
        {
            StopCoroutine("Note");
            Time.timeScale = 1;

          
            if (!_missions[_currentMission - 1].GetComponent<MissionID>()._operaioSconfitto)
            {
                if (KeySequence._mossa == "rise")
                {
                    //Debug.Log("MOSSA SBAGLIATA!!!");
                    StartCoroutine("MossaSbagliata");
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
                    //Debug.Log("MOSSA SBAGLIATA!!!");
                    StartCoroutine("MossaSbagliata");
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


    public IEnumerator Note()
    {

        //yield return new WaitForSecondsRealtime(1);
        //if (!ThirdPersonUnityCharacterController._playingFlute)
        //{
        //    Time.timeScale = 1;
        //    yield break;//Stop coroutine
        //}

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
        Debug.Log(seconds++);
        _allertTime.SetActive(false);
        //Compute position for next time
        Vector3 spwanPosition = new Vector3(0, 1, 0);
        MascheraAlbero.transform.localPosition = MascheraAlbero.transform.localPosition - spwanPosition;
        
        if(MascheraAlbero.transform.localPosition.y < 35)
        {
            _allertTime.SetActive(true);
        }
        

        if (MascheraAlbero.transform.localPosition.y < 25) 
        {
            Time.timeScale = 0;

            //if (SceneManager.GetActiveScene().name == "Tutorial_1")
            //{
            //    SceneManager.LoadScene("Tutorial_1");
            //    Time.timeScale = 0;
            //}
            //else if (SceneManager.GetActiveScene().name == "Tutorial_2")
            //{
            //    SceneManager.LoadScene("Tutorial_2");
            //    Time.timeScale = 0;
            //}
            //else 
            //{
            //    _cutScene.LaunchCutScene("failed");
            //    //SceneManager.LoadScene("ScenaEsplorazioneBosco_Animale");
            //}

            _cutScene.LaunchCutScene("failed");
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
            //ChangeColor._safe = false;
            //if (SceneManager.GetActiveScene().name == "Tutorial_1")
            //{
            //    SceneManager.LoadScene("Tutorial_1");
            //    Time.timeScale = 0;
            //}
            //else {
            //    _cutScene.LaunchCutScene("failed");
            //    //SceneManager.LoadScene("ScenaEsplorazioneBosco_Animale");
            //}
            _cutScene.LaunchCutScene("failed");
        }
        
        _colpita = false;
    }

    public IEnumerator InutileSuonare()
    {
        //yield return new WaitForSecondsRealtime(1f);
        _inutileSuonare.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        _inutileSuonare.SetActive(false);
        yield break;
    }

    public IEnumerator MossaSbagliata()
    {
        yield return new WaitForSecondsRealtime(1f);
        _melodiaSbagliata.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        _melodiaSbagliata.SetActive(false);
        yield break;
    }

    public IEnumerator TroppoLontana()
    {
        //yield return new WaitForSecondsRealtime(1f);
        _troppoDistante.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);
        _troppoDistante.SetActive(false);
        yield break;
    }



}
