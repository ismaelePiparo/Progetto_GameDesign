using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{

    [SerializeField] private List<GameObject> _lives;
    private int i;

    // Start is called before the first frame update
    void Start()
    {
        i = _lives.Count;
        Debug.Log(i);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (i != 0)
            {
                _lives[i - 1].SetActive(false);
                i--;
 
            }
            else
            {
                Debug.Log("sei morto!");
            }
        }
    }
}
