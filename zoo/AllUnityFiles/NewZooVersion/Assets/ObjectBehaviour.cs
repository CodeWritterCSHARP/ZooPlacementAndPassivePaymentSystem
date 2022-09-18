using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBehaviour : MonoBehaviour
{
    public GameObject bm;

    public GameObject cub;

    public int cash;
    public float timer;
    public float maxcash;

    public bool cangivecash;
    private bool changing = false;

    private void Start()
    {
        if (maxcash == 0) cangivecash = true; else cangivecash = false;
        bm = GameObject.Find("ButtinM");
        if (cangivecash == false) InvokeRepeating("MoneyAdding", timer, timer);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0)){
            changing = !changing;
            if(changing == true)
            {
                transform.GetChild(0).gameObject.SetActive(true);
                bm.GetComponent<ButtonMagaze>().selected.Add(this.gameObject);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
                bm.GetComponent<ButtonMagaze>().selected.Remove(this.gameObject);
            }
        }
    }

    void MoneyAdding() { bm.GetComponent<ButtonMagaze>().money += cash;}

    private void Update()
    {
        if (!bm.GetComponent<ButtonMagaze>().gb.activeSelf && changing == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x,
                    gameObject.transform.eulerAngles.y + 90, gameObject.transform.eulerAngles.z);
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                bm.GetComponent<ButtonMagaze>().selected.Remove(this.gameObject);
                bm.GetComponent<ButtonMagaze>().money += cash / 2; Destroy(this.gameObject);
                cub.GetComponent<CubicMouse>().chechfor = true;
            }
        }
    }
}
