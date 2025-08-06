using System;
using LittleLegends.ConponentContainer;
using UnityEngine;

namespace LittleLegends.Characters.States
{
    [CreateAssetMenu(menuName = "Scriptableobject", order = 0)]
    public class CharacterStateSO : ScriptableObject, ICloneable
    {
        [field: SerializeField] public string StateName { get; private set; }
        protected CharacterStateMachine StateMachine { get; private set; }

        public object Clone()
        {
            return Instantiate(this);
        }

        protected T GetComponent<T>() where T : IContainerComponent
        {
            return StateMachine.GetContainerComponent<T>();
        }

        public virtual void Initailize(CharacterStateMachine characterStateMachine)
        {
            StateMachine = characterStateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void Exit()
        {
        }
    }
}