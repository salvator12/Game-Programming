using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CookDash.Managers;
public class ButtonUI : MonoBehaviour
{
    public GameObject[] ui;

    private MejaMakan _mejaMakan;
    public Vector3 posOffset;

    bool wait = false;
    OrderManager orderManager;

    private void Start()
    {
        orderManager = OrderManager.Instance;
    }
    public void enableButton(MejaMakan mejaMakan, bool needMenu, bool serveFood)
    {
        _mejaMakan = mejaMakan;
        if (needMenu)
        {
            ui[0].SetActive(true);
            
        }else if(serveFood)
        {
            ui[1].SetActive(true);
        }
        transform.position = mejaMakan.transform.position + posOffset;
    }

    public void hideButton(bool isRange, bool needMenu, bool serveFood)
    {
        Debug.Log("isRange: " + isRange + ", serveFood: " + serveFood);
        if((!isRange) || (isRange && !needMenu))
        {
            ui[0].SetActive(false);
        }
        if ((!isRange) || (isRange && !serveFood))
        {
            ui[1].SetActive(false);
        }
    }

    public void chooseMenu()
    {
        _mejaMakan.isClicked_needMenu = true;
        _mejaMakan.needMenu = false;
        _mejaMakan.isClicked_giveFood = false;
        hideButton(true, _mejaMakan.needMenu, _mejaMakan.isClicked_giveFood);
        StartCoroutine(giveMenu());
        /*enableButton(_mejaMakan, false, true);*/
    }

    private IEnumerator giveMenu()
    {
        /*_mejaMakan.npc.patience += 10f;*/
        /*Debug.Log("orderManager: " + orderManager);*/
        string orderedFrom = _mejaMakan.name;
        MejaMakan currMejaMakan = _mejaMakan;
        if(!wait)
        {
            wait = true;
            yield return new WaitForSeconds(3f);
            Debug.Log("masuk");
            orderManager.TrySpawnOrder(orderedFrom, currMejaMakan);
        } else
        {
            yield return new WaitForSeconds(3f);
            orderManager.TrySpawnOrder(orderedFrom, currMejaMakan);
            wait = false;
        }
    }

    public void giveFood()
    {

    }
}
