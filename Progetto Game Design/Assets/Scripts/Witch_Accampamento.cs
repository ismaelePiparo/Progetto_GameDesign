using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Witch_Accampamento : MonoBehaviour
{

    [SerializeField] GameObject InteractText;
    [SerializeField] GameObject MissionText;

    private bool _starMission = false;


    [SerializeField] string mission_scene;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_starMission && Input.GetKeyDown(KeyCode.Z))
        {
            InteractText.SetActive(false);
            MissionText.SetActive(true);
        }
        if (_starMission && Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene(mission_scene);
        }
        if (_starMission && Input.GetKeyDown(KeyCode.N))
        {
            InteractText.SetActive(true);
            MissionText.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Premi Y per accettare la missione");
        _starMission = true;
        InteractText.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {

        _starMission = false;
        InteractText.SetActive(false);
        MissionText.SetActive(false);

    }



}
