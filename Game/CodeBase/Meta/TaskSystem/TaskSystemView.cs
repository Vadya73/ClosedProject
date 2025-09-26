using UI;
using UnityEngine;

namespace Meta
{
    public class TaskSystemView : BaseView<IData>
    {
        [SerializeField] private Transform _parentToQuestPrefabs;
        public Transform ParentToQuestPrefabs => _parentToQuestPrefabs;
    }
}
