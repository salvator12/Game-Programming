using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interacable : MonoBehaviour
{
    bool isRange = false;
    bool needMenu;
    bool requestFood;
    private void Update()
    {
        if(isRange)
        {
            if(needMenu)
            {
                Debug.Log("give him menu");   
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            isRange = true;
            Debug.Log("Player in Range");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isRange = false;
            Debug.Log("Player out of Range");
        }
    }
}
