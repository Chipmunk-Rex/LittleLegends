using System;
using LittleLegends.ConponentContainer;
using UnityEngine;

namespace LittleLegends.Characters
{
    public class CharacterAnimationTrigger : MonoBehaviour, IContainerComponent
    {
        public Action OnAnimationEndTrigger;
        public Action OnAnimationEventTrigger;

        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
        }

        private void AnimationEnd() => OnAnimationEndTrigger?.Invoke();
        private void AnimationTrigger() => OnAnimationEventTrigger?.Invoke();
    }
}