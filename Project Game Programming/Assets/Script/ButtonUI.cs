using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ui;

    private MejaMakan _mejaMakan;
    public Vector3 posOffset;
    // Update is called once per frame


    public void enableButton(MejaMakan mejaMakan, bool needMenu, bool serveFood)
    {
        _mejaMakan = mejaMakan;
        if (needMenu)
        {
            ui[0].SetActive(true);
            
        }
        if(serveFood)
        {
            ui[1].SetActive(true);
        }
        transform.position = mejaMakan.transform.position + posOffset;
    }

    public void hideButton(bool isRange, bool needMenu, bool serveFood)
    {
        if((!isRange) || (isRange && !needMenu))
        {
            ui[0].SetActive(false);
        }
        if ((!isRange) || (isRange && !needMenu))
        {
            ui[1].SetActive(false);
        }
    }

    public void chooseMenu()
    {
        _mejaMakan.isClicked_needMenu = true;
        _mejaMakan.needMenu = false;
        hideButton(true, _mejaMakan.needMenu, false);
        StartCoroutine(giveMenu());
        /*enableButton(_mejaMakan, false, true);*/
    }

    private IEnumerator giveMenu()
    {
        yield return new WaitForSeconds(5f);
        int pick = Random.Range(0, 6);
        if (pick == 0)
        {
            Debug.Log("Pilih Menu 0");
        }
        else if (pick == 1)
        {
            Debug.Log("Pilih Menu 1");
        }
        else if (pick == 2)
        {
            Debug.Log("Pilih Menu 2");
        }
        else if (pick == 3)
        {
            Debug.Log("Pilih Menu 3");
        }
        else if (pick == 4)
        {
            Debug.Log("Pilih Menu 4");
        }
        else if (pick == 5)
        {
            Debug.Log("Pilih Menu 5");
        }
    }

    public void giveFood()
    {

    }
}
