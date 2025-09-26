using DG.Tweening;
using UnityEngine;

namespace UI
{
    public abstract class BaseView<T> : MonoBehaviour, IViewable<T> where T : IData
    {
        protected bool _isActive = false;
        protected bool _isAnimated = false;

        public bool IsActive => _isActive;
        public bool IsAnimated => _isAnimated;

        protected virtual void Awake()
        {
            SetAnimatedState(false);
        }

        public virtual void Show()
        {
            if (_isAnimated)
                return;
            
            SetAnimatedState(true);
            SetViewActivity(true);
            gameObject.SetActive(true);

            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            DOTween.Sequence()
                .Append(transform.DOScale(new Vector3(1.1f, 1.1f, 1.1f), 0.3f))
                .Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.3f))
                .AppendCallback(() => SetAnimatedState(false));
        }

        public virtual void Hide()
        {
            if (_isAnimated)
                return;
            
            SetAnimatedState(true);
            
            DOTween.Sequence()
                .Append(transform.DOScale(new Vector3(0.1f, 0.1f, 0.1f),0.3f)
                    .OnComplete(() =>
                    {
                        gameObject.SetActive(false);
                        SetViewActivity(false);
                        SetAnimatedState(false);
                    }));
        }
        
        public void ForcedHide()
        {
            gameObject.SetActive(false);
            SetViewActivity(false);
            SetAnimatedState(false);
        }

        public void ForcedShow()
        {
            gameObject.SetActive(true);
            SetViewActivity(true);
            SetAnimatedState(false);
        }

        protected void SetViewActivity(bool active) => _isActive = active;
        protected void SetAnimatedState(bool state) => _isAnimated = state;

        public virtual void UpdateView(T taskData) { }
    }
}