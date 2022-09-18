using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneGenerator : MonoBehaviour
{
    public GameObject cubic;
    public GameObject pl;
    public GameObject[] gamemas;
    private GameObject curpl;

    private GameObject bm;

    private Vector3 v;

    private bool ch = true;

    private void Start()
    {
        bm = GameObject.Find("ButtinM");

        for (int i = 0; i < 24; i += 2)
        {
            for (int j = 0; j < 24; j += 2)
            {
                v = new Vector3(i, 0, j);
                GameObject gameObj = Instantiate(cubic, v, Quaternion.identity);
                bm.GetComponent<ButtonMagaze>().cubics.Add(gameObj);
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            ch = !ch;
            if (ch == false)
            {
                Instantiate(pl, new Vector3(0, 1, 0), Quaternion.identity);
                for (int i = 0; i < gamemas.Length; i++) gamemas[i].SetActive(false);
                Cursor.lockState = CursorLockMode.Locked; Cursor.visible = false;
            }
            else
            {
                curpl = GameObject.FindGameObjectWithTag("Player");
                Destroy(curpl);
                for (int i = 0; i < gamemas.Length; i++) gamemas[i].SetActive(true);
                Cursor.lockState = CursorLockMode.None; Cursor.visible = true;
            }
        }
    }
}
