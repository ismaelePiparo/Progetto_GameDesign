using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeySequence : MonoBehaviour
{
    private KeyCode[] _sequence = new KeyCode[]{
         KeyCode.P,
         KeyCode.O,
         KeyCode.I
    };
    private int sequenceIndex;
    public static bool _isCorrect = false;
    public static bool _decreaseLives = false;
    [SerializeField] private GameObject _pentagram;
    [SerializeField] private List<GameObject> _notes;

    private int i = 0;
    

    private void Start()
    {
        i = _notes.Count;
    }

    private void Update()
    {
        
        _decreaseLives = false;
        if (Input.GetKeyDown(_sequence[sequenceIndex]) && _pentagram.activeSelf)
        {
            _notes[i - 1].SetActive(true);
            if (++sequenceIndex == _sequence.Length)
            {
                _isCorrect = true;
                _decreaseLives = true;
                sequenceIndex = 0;
            }
            i--;
        }
        else if (Input.anyKeyDown) 
        {
            i = _notes.Count;
            sequenceIndex = 0;
        }
       


    }

}
