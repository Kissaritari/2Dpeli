using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portaali : MonoBehaviour
{
    [SerializeField] private GameObject gemmiEmpty;
    static public int gemmien_lkm;
    static public bool activity = false;
    private void Start()
    {
        gemmien_lkm = gemmiEmpty.transform.childCount;
    }
    // Update is called once per frame
    void Update()
    {
        if (Player.pisteet >= gemmien_lkm)
        {
            activity = true;
            GetComponent<Animator>().Play("open");
        }
            
    }
}
