using LittleLegends.Players;
using LittleLegends.ConponentContainer;
using LittleLegends.StatSystem;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Characters
{
    public class CharacterMovement : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [Header("References")]
        [SerializeField] CharacterController characterController;

        [SerializeField] private StatSO moveStat;
        CharacterAnimator characterAnimator;

        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
            characterAnimator = this.Get<CharacterAnimator>();
        }

        public void AfterInit()
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
            characterAnimator.SetDirection(Vector3.zero);
        }
    }
}