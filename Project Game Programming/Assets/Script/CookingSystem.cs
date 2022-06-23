using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CookDash.Data;
public class CookingSystem : MonoBehaviour
{
    public GameObject spawnFood;
    private bool isSpawnFood = false;
    private bool isCooking = false;
    private void Update()
    {
        Debug.Log("spawn: " + isSpawnFood);
        if ((isSpawnFood) && Player.Instance.inRangeSpawnFood && Player.Instance.imageItemHand.sprite == null && Input.GetKeyDown(KeyCode.E))
        {
            Player.Instance.imageItemHand.gameObject.SetActive(true);
            Player.Instance.imageItemHand.sprite = Player.Instance.Food.sprite;
            Player.Instance.imageItemHand.color = Player.Instance.Food.color;
            Player.Instance.Food.sprite = null;
            Player.Instance.Food.color = Color.white;
            Player.Instance.Food.gameObject.SetActive(false);
            isSpawnFood = false;
        }
    }
    public void throwAllPlacebleItem()
    {
        for(int i = 0; i < Player.Instance.ingredientsForCook.Length; i++)
        {
            Player.Instance.ingredientsForCook[i].sprite = null;
            Player.Instance.ingredientsForCook[i].color = Color.white;
            Player.Instance.ingredientsForCook[i].gameObject.SetActive(false);
        }
        Player.Instance.cookingButton[0].SetActive(false);
        Player.Instance.cookingButton[1].SetActive(false);
        Player.Instance.counter = 0;
        Player.Instance.totalIngredient = 0;
        Player.Instance.prevCheck = null;
    }


    public void cookAyamGoreng()
    {
        Debug.Log("isCook: " + isCooking);
        if(!Player.Instance.isPlaceableItem)
        {
            return;
        }
        if (isCooking)
        {
            Debug.Log("masuk ayam");
            return;
        }
        else if (Player.Instance.Food.sprite != null)
        {
            Debug.Log("masukk");
            return;
        }
        isCooking = true;
        Debug.Log("test");
        throwAllPlacebleItem();
        float cookCountdown = GameManager.Instance.level1.orders[0].cookTime;
        StartCoroutine(SpawnCountdown(cookCountdown, GameManager.Instance.level1.orders[0]));
        
    }

    public void cookTumisAyam()
    {
        Debug.Log("isCook2: " + isCooking);
        if (!Player.Instance.isPlaceableItem)
        {
            return;
        }
        if (isCooking)
        {
            Debug.Log("masuk tumis");
            return;
        } else if(Player.Instance.Food.sprite != null)
        {
            Debug.Log("masukkk");
            return;
        }
        isCooking = true;
        throwAllPlacebleItem();
        float cookCountdown = GameManager.Instance.level1.orders[1].cookTime;
        StartCoroutine(SpawnCountdown(cookCountdown, GameManager.Instance.level1.orders[1]));
    }

    IEnumerator SpawnCountdown(float countdown, OrderData food)
    {
        yield return new WaitForSeconds(countdown);
        Player.Instance.Food.sprite = food.Icon;
        Player.Instance.Food.color = food.color;
        Player.Instance.Food.gameObject.SetActive(true);
        isSpawnFood = true;
        isCooking = false;
    }
}
