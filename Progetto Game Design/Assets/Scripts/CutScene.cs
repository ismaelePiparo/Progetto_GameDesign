using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutScene : MonoBehaviour
{
    [SerializeField] private GameObject _mainCam;
    [SerializeField] private GameObject _cutScene1;
    [SerializeField] private bool _test =false;

    
    

    // Start is called before the first frame update
    void Start()
    {
        //_lenght1 = _cutScene1.GetComponent<UnityEngine.Video.VideoPlayer>().length;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!ThirdPersonUnityCharacterController._playingFlute && KeySequence._isCorrect)
        {
            Time.timeScale = 0;
            StartCoroutine("StartCutSene");
        }
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
