using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using CookDash.Data;
using CookDash.Model;
using CookDash.UI;

namespace CookDash.Managers
{
    public class OrderManager : MonoBehaviour
    {
        [SerializeField] private LevelData currentLevel;
        [SerializeField] private Order orderPrefab;
        [SerializeField] private int maxConcurrentOrders = 5;
        public OrdersPanelUI ordersPanelUI;

        private readonly List<Order> _orders = new List<Order>();
        private readonly Queue<Order> _poolOrders = new Queue<Order>();

        private bool _isGeneratorActive;

        public delegate void OrderSpawned(Order order);
        public static event OrderSpawned OnOrderSpawned;
        public static OrderManager Instance;
        public delegate void OrderExpired(Order order);
        public static event OrderExpired OnOrderExpired;

        public delegate void OrderDelivered(Order order);
        public static event OrderDelivered OnOrderDelivered;

        private void Awake()
        {
            
            Instance = this;
        }

        /*private void Update()
        {
            Debug.Log("jumlah: " + ordersPanelUI._ordersUI.Count);
        }*/
        private Order GetOrderFromPool()
        {
            return _poolOrders.Count > 0 ? _poolOrders.Dequeue() : Instantiate(orderPrefab, transform);
        }



        public void Init(LevelData levelData)
        {
            currentLevel = levelData;
            _orders.Clear();
        }

        public void TrySpawnOrder(string orderedFrom, MejaMakan _mejaMakan)
        {
            /*Debug.Log(_mejaMakan.name);*/
            if (_orders.Count < maxConcurrentOrders)
            {
                Debug.Log("_mejaMakan: " + _mejaMakan.name);
                if(_mejaMakan.order == null)
                {
                    var order = GetOrderFromPool();
                    order.Setup(GetRandomOrderData(), orderedFrom);
                    _mejaMakan.order = order;
                    _orders.Add(order);
                    SubscribeEvents(order);
                    OnOrderSpawned?.Invoke(order);
                }
                
            }

            
        }
        private static void SubscribeEvents(Order order)
        {
            order.OnExpired += HandleOrderExpired;
        }

        private static void UnsubscribeEvents(Order order)
        {
            Debug.Log("unsubscribe: " + order);
            order.OnExpired -= HandleOrderExpired;
        }
        private OrderData GetRandomOrderData()
        {
            var randomIndex = Random.Range(0, currentLevel.orders.Count);
            return Instantiate(currentLevel.orders[randomIndex]);
        }

        private static void HandleOrderDelivered(Order order)
        {
            /*Debug.Log("[OrderManager] HandleOrderDelivered");*/
        }

        public void DeactivateExpiredSendBackToPool(Order order)
        {
            order.SetOrderExpired();
            _orders.RemoveAll(x => x.isExpired);
            Destroy(order.gameObject);
            ordersPanelUI.RegroupPanelsLeft();
            UnsubscribeEvents(order);
            _poolOrders.Enqueue(order);
        }

        public void DeactivateSendBackToPool(Order order)
        {
            order.SetOrderDelivered();
            UnsubscribeEvents(order);
            _orders.RemoveAll(x => x.IsDelivered);
            _poolOrders.Enqueue(order);
            ordersPanelUI.RegroupPanelsLeft();
        }
        private static void HandleOrderExpired(Order order)
        {
            // Debug.Log("[OrderManager] HandleOrderExpired");
            OnOrderExpired?.Invoke(order);
        }
        /*public void CheckIngredientsMatchOrder(List<Ingredient> ingredients)
        {
            if (ingredients == null) return;
            List<IngredientType> plateIngredients = ingredients.Select(x => x.Type).ToList();

            // orders are checked by arrival order (arrivalTime is reset when order expires)
            List<Order> orderByArrivalNotDelivered = _orders
                .Where(x => x.IsDelivered == false)
                .OrderBy(x => x.ArrivalTime).ToList();

            for (int i = 0; i < orderByArrivalNotDelivered.Count; i++)
            {
                var order = orderByArrivalNotDelivered[i];

                List<IngredientType> orderIngredients = order.Ingredients.Select(x => x.type).ToList();

                if (plateIngredients.Count != orderIngredients.Count) continue;

                var intersection = plateIngredients.Except(orderIngredients).ToList();

                if (intersection.Count != 0) continue; // doesn't match any plate

                var tip = CalculateTip(order);
                DeactivateSendBackToPool(order);
                OnOrderDelivered?.Invoke(order, tip);
                ordersPanelUI.RegroupPanelsLeft();
                return;
            }
        }

        private static int CalculateTip(Order order)
        {
            var ratio = order.RemainingTime / order.InitialRemainingTime;
            if (ratio > 0.75f) return 6;
            if (ratio > 0.5f) return 4;
            return ratio > 0.25f ? 2 : 0;
        }*/
    }

}

