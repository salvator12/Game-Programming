using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CookDash.Managers;
using CookDash.Model;
public class MejaMakan : MonoBehaviour
{
    bool isRange = false;
    public ButtonUI _buttonUI;
    [HideInInspector]
    public bool needMenu = false;
    [HideInInspector]
    public bool serveFood = false;
    [HideInInspector]
    public bool isNPC;
    [HideInInspector]
    public bool isClicked_needMenu = false;
    public bool isClicked_giveFood = false;
    public PatienceBar npc;
    public Order order;
    public bool orderExpired = false;
    MejaMakanManager mejaMakanManager;
    private void Start()
    {
        mejaMakanManager = MejaMakanManager.instance;
     }

    private void Update()
    {
        /*if(orderExpired)
        {
            order.expired(this.order);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*Debug.Log("NPC: " + isNPC);*/
        if (collision.gameObject.CompareTag("NPC"))
        {
            /*Debug.Log("masuk");*/
            isNPC = true;
        }
        if (collision.gameObject.CompareTag("Player") && isNPC)
        {
            isRange = true;
            if(!isClicked_needMenu)
            {
                needMenu = true;
            } else if(!isClicked_giveFood)
            {
                serveFood = true;
            }
            mejaMakanManager.selectMejaMakan(this, needMenu, serveFood);
            Debug.Log("Player in Range");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isRange = false;
            mejaMakanManager.deselectMejaMakan(isRange, needMenu, serveFood);
            Debug.Log("Player out of Range");
        }
    }
}
