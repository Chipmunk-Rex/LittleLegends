using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LittleLegends.ConponentContainer
{
    public class ComponentContainer : MonoBehaviour
    {
        private Dictionary<Type, IContainerComponent> _components;

        protected virtual void Awake()
        {
            _components = new Dictionary<Type, IContainerComponent>();
            AddComponentToDictionary();
            ComponentInitialize();
            AfterInitialize();
        }

        public void AddComponentToDictionary(IContainerComponent component)
        {
            try
            {
                _components.Add(component.GetType(), component);
            }
            catch (ArgumentException e)
            {
                Debug.LogError(
                    $"ComponentContainer::AddComponentToDictionary : Component of type {component.GetType()} already exists in the container. " +
                    $"Please ensure that each component is unique within the container. Exception: {e.Message}");
                throw;
            }
        }

        private void AddComponentToDictionary()
        {
            GetComponentsInChildren<IContainerComponent>(true).ToList()
                .ForEach(AddComponentToDictionary);
        }

        private void ComponentInitialize()
        {
            _components.Values.ToList().ForEach(compo => compo.Initialize(this));
        }

        private void AfterInitialize()
        {
            _components.Values.OfType<IAfterInitailze>().ToList().ForEach(compo => compo.AfterInit());
        }

        #region GetComponentRegion

        new public T GetComponent<T>() where T : IContainerComponent
        {
            if (_components.TryGetValue(typeof(T), out IContainerComponent component))
                return (T)component;

            return default(T);
        }

        public T GetSubclassComponent<T>()
        {
            Type findType = _components.Keys.FirstOrDefault(type => typeof(T).IsAssignableFrom(type));
            if (findType != null)
                return (T)_components[findType];

            return default(T);
        }

        public T GetComponent<T>(bool isDerived) where T : IContainerComponent
        {
            IContainerComponent component = GetComponent<T>();
            if (component != null)
                return (T)component;

            if (isDerived == false) return default(T);

            Type findType = _components.Keys.FirstOrDefault(type => type.IsSubclassOf(typeof(T)));
            if (findType != null)
                return (T)_components[findType];

            return default(T);
        }

        public T GetCompo<T>(bool isDerived = false) where T : IContainerComponent
        {
            return GetComponent<T>(isDerived);
        }

        public T Get<T>(bool isDerived = false) where T : IContainerComponent
        {
            return GetComponent<T>(isDerived);
        }

        public bool TryGetComponent<T>(out T component, bool isDerived = false) where T : IContainerComponent
        {
            component = GetComponent<T>(isDerived);
            return component != null;
        }

        public bool TryGetSubclassComponent<T>(out T component)
        {
            component = GetSubclassComponent<T>();
            return component != null;
        }

        #endregion
    }
}