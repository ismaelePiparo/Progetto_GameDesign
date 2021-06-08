using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private List<GameObject> _lives;
    [SerializeField] private GameObject _pentagram;
    [SerializeField] private List<GameObject> _notes;
    

    private int i,n;
    private int _time=5;

    // Start is called before the first frame update
    void Start()
    {
        i = _lives.Count;
        n = _notes.Count;
        Debug.Log(i);
    }

    // Update is called once per frame
    void Update()
    {
        if (ThirdPersonUnityCharacterController._playFlute)
        {
            _time = 5;
            Time.timeScale = 0.5f;
            StartCoroutine("Note");
        }
        if (!_pentagram.activeSelf)
        {
            for (int j = 0; j < n; j++)
            {
                _notes[j].SetActive(false);
            }
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
