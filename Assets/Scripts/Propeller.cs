using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Propeller : MonoBehaviour
{
    //FOr how simple this scrpit is, it too way too long 
    //noting athat I got this scrpit from class 
    public GameObject Propper;
    private Quaternion TargetRoat;
    private float TargetAngel;
    private float Speed;


    private void Start()
    {
        TargetAngel = 0f;
        TargetRoat = Quaternion.AngleAxis(TargetAngel, Vector3.forward);
        Propper.transform.localRotation = TargetRoat;
        Speed = 500f;
        //Note that if speed is too slow, The propeler will not move 
    }

    void Update()
    {
        TargetAngel += Speed *Time.deltaTime;
        TargetRoat = Quaternion.AngleAxis(TargetAngel, Vector3.forward);
        Propper.transform.localRotation = TargetRoat;
    }
}
