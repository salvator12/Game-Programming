using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ui;
    

    public Vector3 posOffset;
    // Update is called once per frame


    public void enableButton(GameObject obj, bool needMenu, bool serveFood)
    {
        
        if(needMenu)
        {
            ui[0].SetActive(true);
            
        }
        if(serveFood)
        {
            ui[1].SetActive(true);
        }
        transform.position = obj.transform.position + posOffset;
    }

    public void hideButton(bool needMenu, bool serveFood)
    {
        if(needMenu)
        {
            ui[0].SetActive(false);
        }
        if (serveFood)
        {
            ui[1].SetActive(false);
        }
    }
}
