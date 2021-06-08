using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySequence : MonoBehaviour
{
    private KeyCode[] _sequence = new KeyCode[]{
         KeyCode.F,
         KeyCode.L,
         KeyCode.A,
         KeyCode.S,
         KeyCode.H
    };
    private int sequenceIndex;
    public static bool _isCorrect = false;
    public static bool _decreaseLives = false;
    [SerializeField] private GameObject _pentagram;
    [SerializeField] private List<GameObject> _notes;

    private int i,c = 0;
    

    private void Start()
    {
        i = _notes.Count;
        c = i;
    }

    private void Update()
    {
        
        _decreaseLives = false;
        if (Input.GetKeyDown(_sequence[sequenceIndex]) && _pentagram.activeSelf)
        {
            _notes[i -c].SetActive(true);
            if (++sequenceIndex == _sequence.Length)
            {
                _isCorrect = true;
                _decreaseLives = true;
                sequenceIndex = 0;
            }
            c--;
        }
        else if (Input.anyKeyDown) 
        {
            i = _notes.Count;
            sequenceIndex = 0;
        }
       


    }

}
