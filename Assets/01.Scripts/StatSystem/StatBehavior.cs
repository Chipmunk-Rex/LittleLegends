using System;
using System.Collections.Generic;
using System.Linq;
using LittleLegends.ConponentContainer;
using UnityEngine;

namespace LittleLegends.StatSystem
{
    public class StatBehavior : MonoBehaviour, IContainerComponent
    {
        [SerializeField] private StatOverride[] statOverrides;
        private Dictionary<string, StatSO> _stats = new();
        public ComponentContainer ComponentContainer { get; set; }

        public void OnInitialize(ComponentContainer componentContainer)
        {
            _stats.Clear();
            foreach (StatOverride statOverride in statOverrides)
            {
                StatSO stat = statOverride.CreateStat();
                _stats.Add(stat.statName, stat);
            }
        }

        public StatSO GetStat(StatSO targetStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : target stat is null");
            return _stats.GetValueOrDefault(targetStat.statName);
        }

        public bool TryGetStat(StatSO targetStat, out StatSO outStat)
        {
            Debug.Assert(targetStat != null, "Stats::GetStat : target stat is null");

            outStat = _stats.GetValueOrDefault(targetStat.statName);
            return outStat;
        }

        public void SetBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue = value;
        public float GetBaseValue(StatSO stat) => GetStat(stat).BaseValue;
        public void IncreaseBaseValue(StatSO stat, float value) => GetStat(stat).BaseValue += value;
        public void AddModifier(StatSO stat, object key, float value) => GetStat(stat).AddModifier(key, value);
        public void RemoveModifier(StatSO stat, object key) => GetStat(stat).RemoveModifier(key);

        public void CleanAllModifier()
        {
            foreach (StatSO stat in _stats.Values)
            {
                stat.ClearAllModifier();
            }
        }


        #region Save logic

        [Serializable]
        public struct StatSaveData
        {
            public string statName;
            public float baseValue;
        }

        public List<StatSaveData> GetSaveData()
            => _stats.Values.Aggregate(new List<StatSaveData>(), (saveList, stat) =>
            {
                saveList.Add(new StatSaveData { statName = stat.statName, baseValue = stat.BaseValue });
                return saveList;
            });


        public void RestoreData(List<StatSaveData> loadedDataList)
        {
            foreach (StatSaveData loadData in loadedDataList)
            {
                StatSO targetStat = _stats.GetValueOrDefault(loadData.statName);
                if (targetStat != default)
                {
                    targetStat.BaseValue = loadData.baseValue;
                }
            }
        }

        #endregion

        public List<StatSO> GetAllStats() => _stats.Values.ToList();
    }
}