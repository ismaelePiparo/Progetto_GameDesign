using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySequence : MonoBehaviour
{
    private KeyCode[] _flash = new KeyCode[]{
         KeyCode.F,
         KeyCode.L,
         KeyCode.A,
         KeyCode.S,
         KeyCode.H
    };

    private KeyCode[] _wind = new KeyCode[]{
         KeyCode.W,
         KeyCode.I,
         KeyCode.N,
         KeyCode.D,
         KeyCode.D
    };

    private KeyCode[] _quake = new KeyCode[]{
         KeyCode.Q,
         KeyCode.U,
         KeyCode.A,
         KeyCode.K,
         KeyCode.E
    };

    private KeyCode[] _rise = new KeyCode[]{
         KeyCode.R,
         KeyCode.I,
         KeyCode.S,
         KeyCode.E,
         KeyCode.E
    };



    private int sequenceIndex=0;

    public static bool _isCorrect = false;
    public static bool _cureTree = false;

    [SerializeField] private GameObject _pentagram;
    [SerializeField] private List<GameObject> _notes;
    [SerializeField] private bool flash =true;
    [SerializeField] private bool wind = false;
    [SerializeField] private bool quake =false;
    [SerializeField] private bool rise=false;
    [SerializeField] private GameObject Flash;
    [SerializeField] private GameObject Wind;
    [SerializeField] private GameObject Quake;
    [SerializeField] private GameObject Rise;

    [SerializeField] private List<AudioClip> _suoni;
    [SerializeField] private List<AudioClip> _suoniBrutti;
    [SerializeField] private AudioSource _audioSource;



    private int i,c,s,sb, rnd = 0;
    public static string  _mossa = "";

    
    private void Start()
    {
        i = _notes.Count;
        c = i;
        Flash.SetActive(flash);
        Wind.SetActive(wind);
        Quake.SetActive(quake);
        Rise.SetActive(rise);
        s = _suoni.Count;
        sb= _suoniBrutti.Count;
        //_audioSource.clip = _suoni[0];
        //_audioSource.Play();

    }

    private void Update()
    {
        //rnd = Random.Range(0,s);
        //Debug.Log(rnd);

        if (Input.GetKeyDown(_flash[sequenceIndex]) && _pentagram.activeSelf && flash)
        {
            rnd = Random.Range(0,s);
            _notes[i - c].SetActive(true);
            _audioSource.clip = _suoni[rnd];
            _audioSource.Play();
            if (++sequenceIndex == _flash.Length)
            {
                _isCorrect = true;
                sequenceIndex = 0;
                _mossa = "flash";
                
                //Debug.Log(_mossa);
            }
            c--;
        }
        else if (Input.GetKeyDown(_wind[sequenceIndex]) && _pentagram.activeSelf && wind)
        {
            
            rnd = Random.Range(0, s);
            _notes[i - c].SetActive(true);
            _audioSource.clip = _suoni[rnd];
            _audioSource.Play();
            if (++sequenceIndex == _wind.Length-1)
            {
                _isCorrect = true;
                sequenceIndex = 0;
                _mossa = "wind";
                
                //Debug.Log(_mossa);
            }
            c--;
        }
        else if (Input.GetKeyDown(_quake[sequenceIndex]) && _pentagram.activeSelf && quake)
        {
            
            rnd = Random.Range(0, s);
            _notes[i - c].SetActive(true);
            _audioSource.clip = _suoni[rnd];
            _audioSource.Play();
            if (++sequenceIndex == _quake.Length)
            {
                _isCorrect = true;
                sequenceIndex = 0;
                _mossa = "quake";
                
                //Debug.Log(_mossa);
            }
            c--;
        }
        else if (Input.GetKeyDown(_rise[sequenceIndex]) && _pentagram.activeSelf && rise)
        {
            
            rnd = Random.Range(0, s);
            _notes[i - c].SetActive(true);
            _audioSource.clip = _suoni[rnd];
            _audioSource.Play();
            if (++sequenceIndex == _rise.Length - 1)
            {
                _isCorrect = true;
                sequenceIndex = 0;
                _mossa = "rise";
                
                //Debug.Log(_mossa);
            }
            c--;
        }
        else if (Input.anyKeyDown || !_pentagram.activeSelf)
        {
           if( _pentagram.activeSelf)
           {
                rnd = Random.Range(0, sb);
                _audioSource.clip = _suoniBrutti[rnd];
                _audioSource.Play();
           }
            for (int j = 0; j < i; j++)
            {
                _notes[j].SetActive(false);
            }
            i = _notes.Count;
            c = i;
            sequenceIndex = 0;
        }

       



    }

}
