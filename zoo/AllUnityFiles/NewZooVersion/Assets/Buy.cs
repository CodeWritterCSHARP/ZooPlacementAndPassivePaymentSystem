using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buy : MonoBehaviour
{
    private GameObject bm;
    public int cost;

    public void Buying()
    {
        bm = GameObject.Find("ButtinM");

        if(bm.GetComponent<ButtonMagaze>().money >= cost)
        {
            bm.GetComponent<ButtonMagaze>().m = true;
            if (bm.GetComponent<ButtonMagaze>().delay == true) bm.GetComponent<ButtonMagaze>().money -= cost;
        }
        else bm.GetComponent<ButtonMagaze>().m = false;
    }
}
