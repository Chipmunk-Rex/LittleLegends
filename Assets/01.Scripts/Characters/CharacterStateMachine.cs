using System;
using System.Collections.Generic;
using Unity.Netcode;
using LittleLegends.ConponentContainer;
using LittleLegends.Characters.States;
using UnityEngine;
using UnityEngine.Serialization;
using Unity.Collections;

namespace LittleLegends.Characters
{
    public class CharacterStateMachine : NetworkBehaviour, IContainerComponent, IAfterInitailze
    {
        [SerializeField] private CharacterStateSO[] OriginalStates;
        private CharacterStateSO CurrentState { get; set; }

        private Dictionary<string, CharacterStateSO> states = new();
        public ComponentContainer ComponentContainer { get; set; }

        public NetworkVariable<FixedString32Bytes> CurrentStateName =
            new NetworkVariable<FixedString32Bytes>(string.Empty,
                NetworkVariableReadPermission.Everyone,
                NetworkVariableWritePermission.Owner);

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

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            if (IsOwner == false)
                CurrentStateName.OnValueChanged += OnNetworkStateChanged;
        }

        private void OnNetworkStateChanged(FixedString32Bytes oldValue, FixedString32Bytes newValue)
        {
            Debug.Log("OnNetworkStateChanged " + newValue.Value);
            if (CurrentState != null && CurrentState.StateName == newValue.Value)
                return;
            ChangeState(newValue.Value);
        }

        public void ChangeState(CharacterStateSO state)
        {
            if (CurrentState == state) return;
            CurrentState?.Exit();
            CurrentState = states[state.StateName];
            CurrentState.Enter();
            if (IsOwner)
                CurrentStateName.Value = state.StateName;
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