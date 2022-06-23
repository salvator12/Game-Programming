using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Lean.Transition;
using UnityEngine.UI;
using System.Threading.Tasks;
using UnityEngine.Assertions;
using CookDash.Model;
using TMPro;
namespace CookDash.UI
{
    public class OrderUI : MonoBehaviour
    {
        [SerializeField] private RectTransform rootRectTransform;
        [SerializeField] private RectTransform basePanel;
        [SerializeField] private RectTransform bottomPanel;
        [SerializeField] private Image orderImage;
        [SerializeField] private TextMeshProUGUI _orderFrom;

        [SerializeField] private List<Image> ingredientImages = new List<Image>();
        [SerializeField] private Image[] images;

        private const float UIWidth = 196f;
        private Vector2 _bottomPanelInitialAnchoredPosition;
        private const int ShakeIntervalTimeMs = 600;
        private bool _shake;

        public float CurrentAnchorX { get; private set; }
        public float SizeDeltaX => rootRectTransform.sizeDelta.x;
        public Order Order { get; private set; }

        private void Start()
        {
            rootRectTransform = GetComponent<RectTransform>();
            _bottomPanelInitialAnchoredPosition = bottomPanel.anchoredPosition;
        }

        public void SlideLeft(float desiredX)
        {
            CurrentAnchorX = desiredX;
            float initialSlideDuration = 0.5f;

            rootRectTransform
                .anchoredPositionTransition_x(desiredX, initialSlideDuration, LeanEase.Decelerate);
        }
        private void HandleExpired(Order order)
        {
            StopShake();
            SlideUp();
            Deactivate();
        }

        /*private void HandleAlertTime(Order order)
        {
            StartShake();
        }*/

        public void Setup(Order order)
        {
            Order = order;
            rootRectTransform.anchoredPosition = new Vector2(Screen.width + 300f, 0f);
            var sizeDelta = rootRectTransform.sizeDelta;
            rootRectTransform.sizeDelta = new Vector2(UIWidth, sizeDelta.y);
            var randomRotation = Random.Range(-45f, +45f);
            basePanel.localRotation = Quaternion.Euler(0f, 0f, randomRotation);

            basePanel.localPosition = new Vector3(basePanel.localPosition.x + 100f, -50f, 0f);

            orderImage.sprite = Order.OrderData.Icon;
            _orderFrom.text = Order._orderFrom;

            for (var i = 0; i < Order.OrderData.ingredients.Count; i++)
            {
                ingredientImages[i].sprite = Order.OrderData.ingredients[i].Icon;
                ingredientImages[i].gameObject.SetActive(true);
            }
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            Order.OnDelivered += HandleDelivered;
            Order.OnExpired += HandleExpired;
        }
        private void UnsubscribeEvents()
        {
            Order.OnDelivered -= HandleDelivered;
            Order.OnExpired -= HandleExpired;
        }
        private void HandleDelivered(Order order)
        {
            HandleDeliveredAsync(order);
        }

        private void HandleDeliveredAsync(Order order)
        {
            SlideUp();
            Deactivate();
        }

        private void SlideUp()
        {
            const float deltaY = 400f;
            basePanel
                .localScaleTransition_xy(new Vector2(1.1f, 1.1f), .3f, LeanEase.Bounce)
                .JoinTransition()
                .localPositionTransition_y(deltaY, .5f, LeanEase.Decelerate);
        }

        private void Deactivate()
        {
            StopShake();
            UnsubscribeEvents();
            bottomPanel.anchoredPosition = _bottomPanelInitialAnchoredPosition;
        }
        public void SlideInSpawn(float desiredX)
        {
            CurrentAnchorX = desiredX;
            float initialSlideDuration = 1f;

            Vector2 small = new Vector2(0.8f, 1f);

            rootRectTransform
                .anchoredPositionTransition_x(desiredX, initialSlideDuration, LeanEase.Decelerate)
                .JoinTransition();

            basePanel.
                localRotationTransition(Quaternion.identity, initialSlideDuration, LeanEase.Decelerate)
                .JoinTransition()
                .localScaleTransition_xy(small, 0.125f, LeanEase.Elastic)
                .JoinTransition()
                .localScaleTransition_xy(Vector2.one, 0.125f, LeanEase.Smooth);

            bottomPanel
                .JoinDelayTransition(initialSlideDuration + 0.25f)
                .JoinTransition()
                .anchoredPositionTransition_y(-120f, 0.3f, LeanEase.Bounce);
        }

        private void StartShake()
        {
            _shake = true;
            ShakeAsync();
        }

        private async Task ShakeAsync()
        {
            const float deltaX = 7f;

            while (_shake)
            {
                basePanel
                    .anchoredPositionTransition_x(-deltaX, 0.15f)
                    .JoinTransition()
                    .anchoredPositionTransition_x(deltaX, 0.3f)
                    .JoinTransition()
                    .anchoredPositionTransition_x(0, 0.15f)
                    .JoinTransition();
                await Task.Delay(ShakeIntervalTimeMs);
            }

            basePanel.anchoredPositionTransition_x(0, 0.15f);
        }

        private void StopShake() => _shake = false;
    }
}

