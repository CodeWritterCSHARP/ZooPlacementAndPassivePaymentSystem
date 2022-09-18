using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMagaze : MonoBehaviour
{
    public GameObject gb;
    bool b = false;

    public GameObject help;
    bool h = false;

    public int count = 1;

    public List<GameObject> list = new List<GameObject>();
    public GameObject[] spawn;
    public GameObject btnL;
    public GameObject btnR;
    public GameObject sp;
    public GameObject tables;

    public Camera cam;

    public string j; public int timeresult;

    public Text txt;

    public bool canplace = false;
    public bool delay = true;

    public int money = 100; public bool m = true;

    public List<GameObject> selected = new List<GameObject>(); int _Scount = 0;

    public List<GameObject> cubics = new List<GameObject>();

    private void FixedUpdate()
    {
        txt.text = money.ToString();
        if(count == list.Count) { btnR.SetActive(false); btnL.SetActive(true); }
        if(count == 1) { btnL.SetActive(false); btnR.SetActive(true); }
    }

    private void Update()
    {
        if(selected.Count == 2 && Input.GetKeyDown(KeyCode.T))
        {
            Vector3 currenttransform = selected[0].transform.position;
            selected[0].transform.position = selected[1].transform.position;
            selected[1].transform.position = currenttransform;
        }
    }

    void Invoking()
    {
        _Scount = 0;
        for (int i = 0; i < selected.Count; i++) if (selected[i] != null) _Scount++;
    }

    public void Open()
    {
        b = !b;
        if (b == true) gb.SetActive(true);
        else gb.SetActive(false);
    }

    public void OpenHelp()
    {
        h = !h;
        if (h == true) help.SetActive(true);
        else help.SetActive(false);
    }

    public void SwapperRight()
    {
        if (count == 1) btnL.SetActive(true);
        list[count - 1].SetActive(false);
        list[count].SetActive(true);
        count++;
    }

    public void SwapperLeft()
    {
        if (count == list.Count) btnR.SetActive(true);
        list[count - 1].SetActive(false);
        list[count-2].SetActive(true);
        count--;
    }

    public void Place()
    {
        if(delay == true && m == true)
        {
            gb.SetActive(false);
            canplace = true;
            sp = Instantiate(spawn[count-1], Vector3.zero, Quaternion.identity);
        }
        if (delay == true && m == false) tables.SetActive(true);
    }

    public IEnumerator PasteWaiting()
    {
        delay = false;
        yield return new WaitForSeconds(1f);
        delay = true;
    }

    #region SavingSystem
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("time", Convert.ToString(DateTime.Now));
        PlayerPrefs.SetInt("money", money);

        List<GameObject> items = new List<GameObject>(); items.Add(this.gameObject);
        items.AddRange(GameObject.FindGameObjectsWithTag("GameController"));
        items.AddRange(GameObject.FindGameObjectsWithTag("Panelss"));

        for (int i = 1; i < items.Count; i++)
        {
            PlayerPrefs.SetFloat("object"+i.ToString(),
                items[i].GetComponent<ObjectBehaviour>().timer * items[i].GetComponent<ObjectBehaviour>().cash);
            PlayerPrefs.SetFloat("maxcash" + i.ToString(), items[i].GetComponent<ObjectBehaviour>().maxcash);

            PlayerPrefs.SetFloat("PosX" + i.ToString(), items[i].transform.position.x);
            PlayerPrefs.SetFloat("PosZ" + i.ToString(), items[i].transform.position.z);
            PlayerPrefs.SetFloat("RotY" + i.ToString(), items[i].transform.eulerAngles.y);
            PlayerPrefs.SetString(i.ToString(), items[i].name[0].ToString());
        }
        PlayerPrefs.SetInt("MAsLength", items.Count);

        List<int> cubicpos = new List<int>();
        for (int i = 0; i < cubics.Count; i++)
            if (cubics[i].GetComponent<CubicMouse>().chechfor == false) cubicpos.Add(i);
        PlayerPrefs.SetInt("CubicLength", cubicpos.Count);
        for (int i = 0; i < cubicpos.Count; i++)
            PlayerPrefs.SetInt("Cubic" + i.ToString(), cubicpos[i]);
    }

    private void Start()
    {
        InvokeRepeating("Invoking", 1,1);
        if(Application.isPlaying /*|| EditorApplication.isPlaying*/)
        {
            j = PlayerPrefs.GetString("time"); TimeSpan value = DateTime.Now.Subtract(Convert.ToDateTime(j));
            timeresult = (int)value.TotalSeconds;
            float moneyplus = 0;

            List<int> indexes = new List<int>(); 
            for (int i = 0; i < PlayerPrefs.GetInt("CubicLength"); i++)
            {
                indexes.Add(PlayerPrefs.GetInt("Cubic" + i.ToString()));
                cubics[indexes.Last()].GetComponent<CubicMouse>().chechfor = false;
            }
            int indexCur = 0;

            for (int i = 1; i < PlayerPrefs.GetInt("MAsLength"); i++)
            {
                float f = PlayerPrefs.GetFloat("object" + i.ToString());
                f = timeresult / f * 50; Debug.Log(f);

                if (f < PlayerPrefs.GetFloat("maxcash" + i.ToString())) moneyplus += f;
                else moneyplus += PlayerPrefs.GetFloat("maxcash" + i.ToString());

                GameObject gb = (GameObject)Resources.Load(PlayerPrefs.GetString(i.ToString()));

                GameObject cur = Instantiate(gb, new Vector3(PlayerPrefs.GetFloat("PosX" + i.ToString()), 0, PlayerPrefs.GetFloat("PosZ" + i.ToString())),
                    Quaternion.AngleAxis(PlayerPrefs.GetFloat("RotY" + i.ToString()), Vector3.up));
                cur.GetComponent<ObjectBehaviour>().cub = cubics[indexes[indexCur]];
                indexCur++;
            }

            Debug.Log(timeresult); Debug.Log(PlayerPrefs.GetInt("MAsLength")); Debug.Log(moneyplus);
            money = PlayerPrefs.GetInt("money") + Convert.ToInt32(moneyplus);
        }
    }
    #endregion
}
