using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMission: MonoBehaviour
{
    [SerializeField] public string _scenaMissione;

    private bool _starMission = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     
        if(_starMission && Input.GetKeyDown(KeyCode.Y))
        {
           
            SceneManager.LoadScene(_scenaMissione);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Premi Y per accettare la missione");
        _starMission = true;
       
        
        
    }

    private void OnTriggerExit(Collider other)
    {

        _starMission = false;
       
    }
}
