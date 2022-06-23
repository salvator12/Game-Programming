using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CookDash.Data;
using CookDash.Managers;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private OrderManager orderManager;
    public LevelData level1;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        orderManager = OrderManager.Instance;
        orderManager.Init(level1);
    }
}
