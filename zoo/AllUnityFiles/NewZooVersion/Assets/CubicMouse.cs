using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubicMouse : MonoBehaviour
{
    public bool chechfor = true;

    private GameObject bm;
    private GameObject current;

    private void Start()
    {
        bm = GameObject.Find("ButtinM");
    }

    private void OnMouseOver()
    {
        if(chechfor == true)
        {
            if (bm.GetComponent<ButtonMagaze>().sp != null)
            {
                bm.GetComponent<ButtonMagaze>().sp.transform.position = gameObject.transform.position;

                if(bm.GetComponent<ButtonMagaze>().canplace == true && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    chechfor = false;
                    current = Instantiate(bm.GetComponent<ButtonMagaze>().sp, gameObject.transform.position, Quaternion.identity);
                    //current.AddComponent<BoxCollider>();
                    current.GetComponent<ObjectBehaviour>().cub = this.gameObject;

                    Destroy(bm.GetComponent<ButtonMagaze>().sp);
                    StartCoroutine(bm.GetComponent<ButtonMagaze>().PasteWaiting());

                    bm.GetComponent<ButtonMagaze>().canplace = false;
                    bm.GetComponent<ButtonMagaze>().gb.SetActive(true);
                }
            }
        }
    }
}
