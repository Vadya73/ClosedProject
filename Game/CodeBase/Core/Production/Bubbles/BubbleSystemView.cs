using UnityEngine;

namespace Core
{
    public class BubbleSystemView : MonoBehaviour
    {
        [SerializeField] private Transform _bubbleContainer;
        
        public Transform BubbleContainer => _bubbleContainer;
    }
}