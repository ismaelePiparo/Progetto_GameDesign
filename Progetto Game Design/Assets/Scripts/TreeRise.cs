using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRise : MonoBehaviour
{


    private int _id=0;
    public bool _curato = false;
    public bool _foglieAttive = false;

    [SerializeField] private GameObject foglie;

    // Start is called before the first frame update
    void Start()
    {
        _id = GetComponentInParent<MissionID>().ID;

    }

    // Update is called once per frame
    void Update()
    {
        if (GameController._alberoCurato && ThirdPersonUnityCharacterController._IDtarget == _id)
        {
            StartCoroutine("Foglie");
        }
        if (foglie.activeSelf)
        {
            StartCoroutine("Timer");
        }
    }

    public IEnumerator Foglie()
    {
        yield return new WaitForSecondsRealtime(10);
        foglie.SetActive(true);

    }

    public IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(5);
        _foglieAttive = true;

    }
}
