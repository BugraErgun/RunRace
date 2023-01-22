using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoints : MonoBehaviour
{
    [HideInInspector]
    public GameObject[] checkPoints;
    [HideInInspector]
    public int currentCheckPoints = 1;

    void Awake()
    {
        checkPoints = GameObject.FindGameObjectsWithTag("CheckPoint");
        currentCheckPoints = 1;
    }

    void Start()
    {
        foreach (GameObject cp in checkPoints)
        {
            cp.AddComponent<CurrentCheckPoint>();
            cp.GetComponent<CurrentCheckPoint>().currentCheckPointNo = currentCheckPoints;
            cp.name = "CheckPoint" + currentCheckPoints;
            currentCheckPoints++;


        }
    }
}
