using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenObject : MonoBehaviour
{

    [SerializeField] private List<GameObject> _hiddenObjects;

    private int c = 0;

    private void Awake()
    {
        c = _hiddenObjects.Count;
        for (int i = 0; i < c; i++)
        {
            
            if (_hiddenObjects[i].activeSelf)
            {
                _hiddenObjects[i].SetActive(false);
            }
            

        }
    }


    // Start is called before the first frame update
    void Start()
    {
        c = _hiddenObjects.Count;
        Spawn(c);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(int c)
    {
        int x;
        
       

        for (int i=0; i<5; i++)
        {
            x = Random.Range(0, c);
            if (_hiddenObjects[x].activeSelf)
            {
                i = i - 1;
            }
            else
            {
                _hiddenObjects[x].SetActive(true);
            }
            
        }
    }
}
