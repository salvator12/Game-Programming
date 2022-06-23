using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CookDash.Data;
public class Player : MonoBehaviour
{
    public static Player Instance;

    Ingredients ingredientsHand;
    OrderData orderHand;
    public Image imageItemHand;
    public Image Food;
    public Image[] ingredientsForCook;
    public GameObject[] cookingButton; 
    private bool isThrow = false;
    public bool isPlaceableItem = false;
    [HideInInspector]
    public IngredientsData prevCheck = null;
    [HideInInspector]
    public int totalIngredient = 0;
    [HideInInspector]
    public int counter = 0;
    [HideInInspector]
    public bool inRangeSpawnFood = false;
    [HideInInspector]
    
    private void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log("item: " + ingredientsHand);*/
        if ((imageItemHand.sprite == null && ingredientsHand != null) && Input.GetKeyDown(KeyCode.E))
        {
            setingredientHand();
        } else if (isThrow && Input.GetKeyDown(KeyCode.G))
        {
            dropitemHand();
        } else if(isPlaceableItem && (imageItemHand.sprite != null && ingredientsHand != null) && Input.GetKeyDown(KeyCode.F))
        {
            putIngredient();
            matchingIngredientandOrder();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("stok ayam") || collision.CompareTag("stok bawang putih"))
        {
            if(imageItemHand.sprite == null && ingredientsHand == null)
            {
                ingredientsHand = collision.GetComponent<Ingredients>();
            }
            /*Debug.Log("collision: " + ingredientsHand);*/
            
        }else if(collision.CompareTag("tong sampah"))
        {
            isThrow = true;
        }else if(collision.CompareTag("placeableIngredient"))
        {
            isPlaceableItem = true;
        } else if(collision.CompareTag("foodSpawn"))
        {
            inRangeSpawnFood = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("stok ayam") || collision.CompareTag("stok bawang putih"))
        {
            if(imageItemHand.sprite == null)
            {
                ingredientsHand = null;
            }
            Debug.Log("collision keluar: " + ingredientsHand);

        }
        else if (collision.CompareTag("tong sampah"))
        {
            isThrow = false;
        }
        else if (collision.CompareTag("placeableIngredient"))
        {
            isPlaceableItem = false;
        }
        else if (collision.CompareTag("foodSpawn"))
        {
            inRangeSpawnFood = false;
        }
    }

    public void dropitemHand()
    {
        ingredientsHand = null;
        imageItemHand.sprite = null;
        imageItemHand.color = Color.white;
        imageItemHand.gameObject.SetActive(false);
    }
    public void setingredientHand()
    {
        imageItemHand.gameObject.SetActive(true);
        imageItemHand.sprite = ingredientsHand.ingredientsData.Icon;
        imageItemHand.color = ingredientsHand.color;
    }
    public void putIngredient()
    {
        for(int i = 0; i < ingredientsForCook.Length; i++)
        {
            if(ingredientsForCook[i].sprite == null && imageItemHand != null)
            {
                Debug.Log("sprite: " + imageItemHand.sprite);
                ingredientsForCook[i].sprite = imageItemHand.sprite;
                Debug.Log("spriteingredient: " + ingredientsForCook[i].sprite);
                ingredientsForCook[i].color = imageItemHand.color;
                ingredientsForCook[i].gameObject.SetActive(true);
                ingredientsForCook[i].sprite = imageItemHand.sprite;
                totalIngredient++;
                dropitemHand();
                return;
            }
        }
    }
    public void matchingIngredientandOrder()
    {
        
        for (int j = 0; j < GameManager.Instance.level1.orders.Count; j++)
        {
            Debug.Log("int j: " + j);
            for (int i = 0; i < ingredientsForCook.Length; i++)
            {

                if (ingredientsForCook[i].sprite != null)
                {
                    for (int k = 0; k < GameManager.Instance.level1.orders[j].ingredients.Count; k++)
                    {
                        if (ingredientsForCook[i].sprite == GameManager.Instance.level1.orders[j].ingredients[k].Icon &&
                            GameManager.Instance.level1.orders[j].ingredients[k] != prevCheck && totalIngredient == GameManager.Instance.level1.orders[j].ingredients.Count)
                        {
                            prevCheck = GameManager.Instance.level1.orders[j].ingredients[k];
                            counter++;
                            break;
                        } else
                        {
                            cookingButton[1].SetActive(false);
                            cookingButton[0].SetActive(false);
                        }
                    }
                } else
                {
                    continue;
                }
                Debug.Log("counter: " + counter);
                Debug.Log("j: " + j);
                Debug.Log(GameManager.Instance.level1.orders[j].name);
                Debug.Log(GameManager.Instance.level1.orders[j].ingredients.Count);
                if (counter > 0 && counter == GameManager.Instance.level1.orders[j].ingredients.Count && counter == totalIngredient)
                {
                    Debug.Log("mass");
                    if (GameManager.Instance.level1.orders[j].orderName == cookingButton[0].name)
                    {
                        cookingButton[0].SetActive(true);
                        cookingButton[1].SetActive(false);
                        break;
                    }
                    else if (GameManager.Instance.level1.orders[j].orderName == cookingButton[1].name)
                    {
                        cookingButton[1].SetActive(true);
                        cookingButton[0].SetActive(false);
                        break;
                    }
                }
            }
            if(totalIngredient == counter)
            {
                break;
            }
        }
        
    }
}
