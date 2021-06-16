using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionID : MonoBehaviour
{

    [SerializeField] public int ID = 1;
    [SerializeField] public bool _completed = false;
    [SerializeField] public GameObject Tree;
    [SerializeField] public GameObject Operaio;

    [SerializeField] public bool _curaAncheAlbero = true;


    public bool _operaioSconfitto = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(!Operaio.activeSelf)
        {
            _operaioSconfitto = true;
        }

        if (!Operaio.activeSelf && (Tree.GetComponent<TreeRise>()._foglieAttive || !_curaAncheAlbero))
        {
            _completed = true;
        }
    }

    
}
