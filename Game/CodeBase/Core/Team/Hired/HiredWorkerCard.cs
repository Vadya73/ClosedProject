using System;
using Infrastructure;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class HiredWorkerCard : MonoBehaviour, IPoolable
    {
        [Header("Tech Components")]
        [SerializeField] private Button _moreInfoButton;
        [Header("UI Components")]
        [SerializeField] private TMP_Text _name;
        [SerializeField] private TMP_Text _description;
        [SerializeField] private TMP_Text _salary;
        [SerializeField] private Image _avatar;
        [Header("Data")]
        [SerializeField, ReadOnly] private WorkerConfig _cardConfig;
        
        public WorkerConfig CardConfig => _cardConfig;

        public void UpdateData(WorkerConfig workerData, Action<HiredWorkerCard> onPurchase)
        {
            _cardConfig = workerData;
            
            _name.text = workerData.WorkerData.Name;
            _description.text = workerData.WorkerData.Description;
            _salary.text = "Salary: " + workerData.WorkerData.CurrentSalary.ToString();
            _avatar.sprite = workerData.WorkerData.Avatar;
            
            _moreInfoButton.onClick.AddListener(() => onPurchase(this));
        }

        public void UpdateSalary(long salary)
        {
            _salary.text = "Salary: " + salary.ToString();
        }
        
        public void OnSpawn()
        {
            Debug.Log("Spawn");
        }

        public void OnDespawn()
        {
            _moreInfoButton.onClick.RemoveAllListeners();
            Debug.Log("Despawn");
        }

    }
}