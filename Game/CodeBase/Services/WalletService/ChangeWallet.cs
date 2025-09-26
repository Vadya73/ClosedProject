using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Services
{
    public class ChangeWallet : MonoBehaviour
    {
        [SerializeField] private Color _addColorText;
        [SerializeField] private Color _subtractionColorText;
        [SerializeField] private TMP_Text _changeWalletText;
        [SerializeField] private Vector2 _offset;

        [SerializeField] private Transform _cashObjectTransform;
        [SerializeField] private Transform _tokensObjectTransform;
        [SerializeField] private Transform _greenCardObjectTransform;
        [SerializeField] private Transform _purpleCardObjectTransform;
        
        private void Awake()
        {
            _changeWalletText.color = new Color(1f,1f,1f,0f);
        }

        public void ShowChangeWallet(WalletType walletType,float count)
        {
            if (count > 0)
                ShowAdd(count);
            else
                ShowSubtraction(count);

            gameObject.SetActive(true);

            switch (walletType)
            {
                case WalletType.Cash:
                    PlayAnimation(_cashObjectTransform);
                    break;
                case WalletType.Tokens:
                    PlayAnimation(_tokensObjectTransform);
                    break;
                case WalletType.GreenCard:
                    PlayAnimation(_greenCardObjectTransform);
                    break;
                case WalletType.PurpleCard:
                    PlayAnimation(_purpleCardObjectTransform);
                    break;
            }
        }

        private void PlayAnimation(Transform animTransform)
        {
            transform.position = new Vector3(animTransform.position.x + _offset.x, 
                animTransform.position.y + _offset.y, animTransform.position.z);
                    
            transform.DOMove(animTransform.position, 1f);
            _changeWalletText.DOFade(0, 1f).OnComplete(() => _changeWalletText.color = new Color(1f,1f,1f,0f));
        }

        private void ShowAdd(float count)
        {
            _changeWalletText.color = _addColorText;
            _changeWalletText.text = "+" + count;
        }

        private void ShowSubtraction(float count)
        {
            _changeWalletText.color = _subtractionColorText;
            _changeWalletText.text = count.ToString();
        }
    }
}