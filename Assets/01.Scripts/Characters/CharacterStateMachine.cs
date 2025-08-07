using System;
using System.Collections.Generic;
using LittleLegends.ConponentContainer;
using LittleLegends.Characters.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace LittleLegends.Characters
{
    public class CharacterStateMachine : MonoBehaviour, IContainerComponent, IAfterInitailze
    {
        [SerializeField] private CharacterStateSO[] OriginalStates;
        private CharacterStateSO CurrentState { get; set; }

        private Dictionary<string, CharacterStateSO> states = new();
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
            ComponentContainer = componentContainer;
            foreach (var state in OriginalStates)
            {
                if (!states.ContainsKey(state.name))
                {
                    states[state.StateName] = (CharacterStateSO)state.Clone();
                    states[state.StateName].Initailize(this);
                }
            }

            if (states.Count > 0)
            {
                CurrentState = states[OriginalStates[0].StateName];
                CurrentState.Enter();
            }

            CharacterAnimationTrigger animationTrigger = this.Get<CharacterAnimationTrigger>();
            animationTrigger.OnAnimationEventTrigger += OnAnimationTrigger;
            animationTrigger.OnAnimationEndTrigger += OnAnimationEnd;
        }

        public void AfterInit()
        {
            foreach (CharacterStateSO state in states.Values)
            {
                if (state is IAfterInitailze afterInitializeState)
                {
                    afterInitializeState.AfterInit();
                }
            }
        }

        public void ChangeState(CharacterStateSO state)
        {
            CurrentState?.Exit();
            CurrentState = states[state.StateName];
            CurrentState.Enter();
        }

        public void ChangeState(string stateName)
        {
            if (states.TryGetValue(stateName, out CharacterStateSO newState))
            {
                ChangeState(newState);
            }
        }

        public void Update()
        {
            CurrentState?.Update();
        }

        private void OnAnimationTrigger()
        {
            CurrentState?.OnAnimationTrigger();
        }

        private void OnAnimationEnd()
        {
            CurrentState?.OnAnimationEnd();
        }
    }
}