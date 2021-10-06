using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionScript : MonoBehaviour
{
    GameObject selectedObject;

    //Butonlar parent
    [SerializeField] GameObject buttons;
    Outline outline;

    Vector3 temp ;

    [SerializeField] float speed = 15f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //T�klay�nca ray g�nder
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 80))
            {
                //I��na �arpan obje se�ilebilirse
                if (hit.collider.gameObject.tag == "Selectable")
                {
                    //daha �nce b�yle bir obje varsa
                    if (selectedObject != null)
                    {
                        // outline kapat
                        selectedObject.GetComponent<Outline>().enabled = false;
                        outline = null;
                    }

                    selectedObject = hit.collider.gameObject;

                    if (selectedObject.GetComponent<Outline>() != null)
                    {
                        outline = selectedObject.GetComponent<Outline>();
                        outline.enabled = true;
                    }
                    else if (selectedObject.GetComponent<Outline>() == null)
                    {
                        //OutlineVer
                        outline = selectedObject.AddComponent<Outline>();

                    }

                    //Butonlar� a�
                    buttons.SetActive(true);
                }
            }
        }
    }

    public void B�y�tmeButon()
    {
        if (selectedObject != null)
        {

            temp = selectedObject.transform.localScale;
            temp.x += Time.deltaTime * speed;
            temp.y += Time.deltaTime * speed;
            temp.z += Time.deltaTime * speed;
            selectedObject.transform.localScale = temp;
        }

    }
    public void K���ltmeButon()
    {
        if (selectedObject != null)
        {
            temp = selectedObject.transform.localScale;
            temp.x -= Time.deltaTime * speed *2;
            temp.y -= Time.deltaTime * speed * 2;
            temp.z -= Time.deltaTime * speed * 2;
            selectedObject.transform.localScale = temp;
        }

    }
}