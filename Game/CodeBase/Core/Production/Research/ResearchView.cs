using System;
using UI;
using UnityEngine;

namespace Core
{
    public class ResearchView : BaseView<SkillData>
    {
        [SerializeField] private Transform _highlightObject;

        public event Action OnEnabled; 
        public Transform HighlightObject => _highlightObject;

        private void OnEnable()
        {
            OnEnabled?.Invoke();
            _highlightObject.gameObject.SetActive(false);
        }
    }
}