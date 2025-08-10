using LittleLegends.Players;
using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Characters
{
    public class CharacterMovement : NetworkBehaviour, IContainerComponent, IAfterInitailze
    {
        [Header("References")] [SerializeField]
        protected CharacterController characterController;

        [SerializeField] private StatSO moveStat;
        CharacterAnimator characterAnimator;

        public ComponentContainer ComponentContainer { get; set; }

        public virtual void OnInitialize(ComponentContainer componentContainer)
        {
            characterAnimator = this.Get<CharacterAnimator>();
        }

        public virtual void AfterInit()
        {
        }

        public void Move(Vector3 moveDirection)
        {
            characterController.Move(moveDirection * (moveStat.Value * Time.deltaTime * 2));
            characterAnimator.SetDirection(moveDirection);
        }

        public void Stop()
        {
            characterAnimator.SetDirection(Vector3.zero);
        }
    }
}