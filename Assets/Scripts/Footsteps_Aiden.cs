using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps_Aiden : MonoBehaviour
{
    public UnityEngine.GameObject obj_footstep1;
    public UnityEngine.GameObject obj_footstep2;

    public void launch_footstep1()
    {
        obj_footstep2.SetActive(false);
        obj_footstep1.SetActive(true);
    }

    public void launch_footstep2()
    {
        obj_footstep1.SetActive(false);
        obj_footstep2.SetActive(true);
    }
}
