using System;
using LittleLegends.ConponentContainer;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Players
{
    public class PlayerController : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [SerializeField] private PlayerInputSO playerInputSo;
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        public void AfterInit()
        {
        }

        private void Update()
        {
            LookMouse();
        }

        public void LookMouse()
        {
            Vector2 mouseScreenPosition = playerInputSo.MousePosition;
            Vector2 characterScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
            Vector2 direction = mouseScreenPosition - characterScreenPosition;
            transform.forward = new Vector3(direction.x, 0, direction.y);
        }
    }
}