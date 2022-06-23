using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CookDash.Data;
using CookDash.Managers;
namespace CookDash.Model
{
    public class Order : MonoBehaviour
    {
        public OrderData _orderData;
        public bool IsDelivered { get; private set; }
        public bool isExpired { get; private set; }
        public OrderData OrderData => _orderData;
        public List<IngredientsData> Ingredients => _orderData.ingredients;

        public delegate void Expired(Order order);
        public event Expired OnExpired;

        public delegate void Delivered(Order order);
        public event Delivered OnDelivered;
        public string _orderFrom;
        public void Setup(OrderData orderData, string orderedFrom)
        {
            IsDelivered = false;
            isExpired = false;
            _orderData = orderData;
            _orderFrom = orderedFrom;
        }

        public void SetOrderExpired()
        {
            isExpired = true;
            OnExpired?.Invoke(this);
        }
    
        public void SetOrderDelivered()
        {
            IsDelivered = true;
            OnDelivered?.Invoke(this);
        }
    }
}

