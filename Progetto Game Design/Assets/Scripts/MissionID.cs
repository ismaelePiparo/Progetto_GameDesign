using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MissionID : MonoBehaviour
{

    [SerializeField] public int ID = 1;
    [SerializeField] public bool _completed = false;
    [SerializeField] public GameObject Tree;
    [SerializeField] public List<GameObject> Operai;

    [SerializeField] public bool _curaAncheAlbero = true;


    public bool _operaioSconfitto = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        int _operaiSconfitti = Operai.Count(item=>!item.activeSelf);
        if (_operaiSconfitti==Operai.Count)
        {
            _operaioSconfitto = true;
        }

        if (_operaiSconfitti == Operai.Count && (Tree.GetComponent<TreeRise>()._foglieAttive || !_curaAncheAlbero))
        {
            _completed = true;
        }
    }

    
}
