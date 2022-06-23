using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using CookDash.Model;
using CookDash.Managers;
public class PatienceBar : MonoBehaviour
{
    public float startPatience;
    [HideInInspector]public float patience;

    public TextMeshProUGUI customerState;
    public Image patienceBar;
    public MejaMakan _mejaMakan;
    public bool stopTimer;
    /*private Order order;*/
    void Start()
    {
        stopTimer = false;
        patience = startPatience;
        StartCoroutine(CountingPatience());
    }

    private IEnumerator CountingPatience()
    {
        while(patience > 0)
        {
            yield return new WaitForSeconds(2f);
            patience -= 2f;
            patienceBar.fillAmount = patience / startPatience;
            if (patience <= 20 && patience > 10)
            {
                customerState.text = "Annoyed";
            }
            else if (patience <= 10)
            {
                customerState.text = "Angry";
            }
            yield return null;
        }
        /*Debug.Log("nama: " + _mejaMakan.name);
        Debug.Log("order: " + _mejaMakan.order);*/
        OrderManager.Instance.DeactivateExpiredSendBackToPool(_mejaMakan.order);
        Destroy(this.gameObject);
        _mejaMakan.isNPC = false;
        _mejaMakan.isClicked_needMenu = false;
    }

    public void giveFood()
    {
        Debug.Log(Player.Instance.imageItemHand.sprite);
        if (Player.Instance.imageItemHand.sprite == _mejaMakan.order.OrderData.Icon)
        {
            OrderManager.Instance.DeactivateSendBackToPool(_mejaMakan.order);
            Destroy(this.gameObject);
            _mejaMakan.isNPC = false;
            _mejaMakan.isClicked_giveFood = false;
        }
    }
    
}
