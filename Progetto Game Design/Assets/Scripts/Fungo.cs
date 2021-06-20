using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fungo : MonoBehaviour
{
    [SerializeField] GameObject InteractText;
    

    private bool _trovato = false;


    



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_trovato && Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log("Fungo_Preso");
            InteractText.SetActive(false);
            this.gameObject.SetActive(false);
            

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        Debug.Log("Premi Z per prendere il fungo");
        _trovato = true;
        InteractText.SetActive(true);

    }

    private void OnTriggerExit(Collider other)
    {
       
        _trovato = false;
        InteractText.SetActive(false);
        

    }
}
