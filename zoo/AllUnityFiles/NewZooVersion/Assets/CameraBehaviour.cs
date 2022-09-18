using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraBehaviour : MonoBehaviour
{
    private Vector3 posit;
    float mover = 0;

    [SerializeField]
    float speed = 0.1f;

    private float timer = 0.7f;
    private int count = 0;

    bool offDC = false;
    public GameObject btn;

    private void FixedUpdate()
    {
        #region Moving
        if (Input.GetKey(KeyCode.D))
        {
            MoveGetterX(); mover += Time.fixedDeltaTime * speed; SetPositionX();
        }
        if (Input.GetKeyUp(KeyCode.D)) speed = 0.1f;

        if (Input.GetKey(KeyCode.A))
        {
            MoveGetterX(); mover -= Time.fixedDeltaTime * speed; SetPositionX();
        }
        if (Input.GetKeyUp(KeyCode.A)) speed = 0.1f;

        if (Input.GetKey(KeyCode.W))
        {
            MoveGetterZ(); mover += Time.fixedDeltaTime * speed; SetPositionZ();
        }
        if (Input.GetKeyUp(KeyCode.W)) speed = 0.1f;

        if (Input.GetKey(KeyCode.S))
        {
            MoveGetterZ(); mover -= Time.fixedDeltaTime * speed; SetPositionZ();
        }
        if (Input.GetKeyUp(KeyCode.S)) speed = 0.1f;
        #endregion

        #region Zooming
        if (Input.GetKey(KeyCode.Q))
        {
            speed += 0.1f;
            gameObject.GetComponent<Camera>().fieldOfView += Time.fixedDeltaTime * speed;
        }
        if (Input.GetKeyUp(KeyCode.Q)) speed = 0.1f;

        if (Input.GetKey(KeyCode.E))
        {
            speed += 0.1f;
            gameObject.GetComponent<Camera>().fieldOfView -= Time.fixedDeltaTime * speed;
        }
        if (Input.GetKeyUp(KeyCode.E)) speed = 0.1f;

        if (Input.GetAxis("Mouse ScrollWheel") != 0f) gameObject.GetComponent<Camera>().fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 10;
        #endregion
    }

    private void Update()
    {
        #region DoubleClick
        if(offDC == false)
            if (Input.GetMouseButtonDown(0))
            {
                count++; if (count == 1) StartCoroutine(enumerator());
            }
        #endregion
    }

    void MoveGetterX() { mover = transform.position.x; speed += 0.1f; }

    void MoveGetterZ() { mover = transform.position.z; speed += 0.1f; }

    void SetPositionX()
    {
        posit = new Vector3(mover, transform.position.y, transform.position.z);
        gameObject.transform.position = posit;
    }

    void SetPositionZ()
    {
        posit = new Vector3(transform.position.x, transform.position.y, mover);
        gameObject.transform.position = posit;
    }

    private IEnumerator enumerator()
    {
        yield return new WaitForSeconds(timer);
        if (count > 1) gameObject.GetComponent<Camera>().fieldOfView /= 1.5f;
        yield return new WaitForSeconds(0.05f);
        count = 0;
    }

    public void offDc()
    {
        offDC = !offDC;
        if (offDC == false) btn.GetComponent<Text>().text = "OffDC";
        else btn.GetComponent<Text>().text = "OnDC";
    }
}
