using System;
using DG.Tweening;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Bubble : MonoBehaviour, IPoolable
    {
        [SerializeField] private Image _backgroundImage;
        private BubbleType _bubbleType;
        private Action<Bubble> OnCompleteMove;
        private Sequence _moveSequence;
        public Sequence ShowSequence { get; set; }
        public BubbleType BubbleType => _bubbleType;
        
        private Transform _objectTransform;


        private void Awake()
        {
            _objectTransform = GetComponent<Transform>();
        }

        public void Move(Vector3 transformPosition, Action<Bubble> onCompleteMove)
        {
            OnCompleteMove = onCompleteMove;
            _moveSequence = DOTween.Sequence()
                .Append(_objectTransform.DOMove(transformPosition, .5f))
                .Append(_objectTransform.DOScale(Vector3.zero, .3f))
                .OnComplete(() => onCompleteMove?.Invoke(this));
        }

        public void SetBubbleType(BubbleType bubbleType, BubblesConfig config)
        {
            _bubbleType = bubbleType;
            
            switch (_bubbleType)
            {
                case BubbleType.AIBlue:
                    _backgroundImage.color = config.AITrainingDataScienceColor;
                    break;
                case BubbleType.ResearchGreen:
                    _backgroundImage.color = config.ResearchColor;
                    break;
                case BubbleType.BROrange:
                    _backgroundImage.color = config.BuildRobotTechnologyColor;
                    break;
                case BubbleType.BRYellow:
                    _backgroundImage.color = config.BuildRobotDesignColor;
                    break;
            }
        }

        public void OnSpawn() { }

        public void OnDespawn()
        {
            _moveSequence.Kill();
            ShowSequence.Kill();
        }

        private void OnDestroy()
        {
            OnCompleteMove?.Invoke(this);
            _moveSequence.Kill();
            ShowSequence.Kill();
        }
    }
}