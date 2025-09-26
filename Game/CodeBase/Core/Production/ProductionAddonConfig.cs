using UnityEngine;

namespace Core
{
    public abstract class ProductionAddonConfig : ScriptableObject, ISkillPoint
    {
        [SerializeField] private string _name;
        [SerializeField] private int _addingDaysToCreate;

        public string Name => _name;
        public int AddingDaysToCreate => _addingDaysToCreate;
    }
}