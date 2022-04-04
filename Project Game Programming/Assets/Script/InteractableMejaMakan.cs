using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableMejaMakan : MonoBehaviour
{
    bool isRange = false;
    public ButtonUI _buttonUI;
    [HideInInspector]
    public bool needMenu = false;
    [HideInInspector]
    public bool serveFood;
    [HideInInspector]
    public bool isNPC;
    /* private void Start()
     {
         _buttonUI = GetComponent<ButtonUI>()
     }
 */
    private void Update()
    {
        if(isRange)
        {
            /*if(_buttonUI.needMenu)
            {
                Debug.Log("give him menu");   
            }*/
        }
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
            needMenu = true;
            _buttonUI.enableButton(this.gameObject, needMenu, serveFood);
            Debug.Log("Player in Range");
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isRange = false;
            _buttonUI.hideButton(needMenu, serveFood);
            Debug.Log("Player out of Range");
        }
    }
}
