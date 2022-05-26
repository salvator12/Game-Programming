using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MejaMakan : MonoBehaviour
{
    bool isRange = false;
    public ButtonUI _buttonUI;
    [HideInInspector]
    public bool needMenu = false;
    [HideInInspector]
    public bool serveFood;
    [HideInInspector]
    public bool isNPC;
    [HideInInspector]
    public bool isClicked_needMenu = false;
    MejaMakanManager mejaMakanManager;
    private void Start()
    {
        mejaMakanManager = MejaMakanManager.instance;
     }

    private void Update()
    {
        /*if (this._buttonUI.isClicked_needMenu)
        {
            needMenu = false;
            _buttonUI.hideButton(isRange, needMenu, serveFood);
        }*/
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(isNPC);
        if (collision.gameObject.CompareTag("NPC"))
        {
            isNPC = true;
        }
        if (collision.gameObject.CompareTag("Player") && isNPC)
        {
            isRange = true;
            if(!isClicked_needMenu)
            {
                needMenu = true;
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
