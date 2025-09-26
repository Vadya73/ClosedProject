using UnityEngine;

namespace Core.CameraControl
{
    [CreateAssetMenu(menuName = "CameraConfig")]
    public class CameraConfig : ScriptableObject
    {
        [Header("Ограничения по X/Z (движение)")]
        public float minX = -10f;
        public float maxX = 10f;
        public float minZ = -10f;
        public float maxZ = 10f;

        [Header("Ограничения по Y (зум)")]
        public float minY = 5f;
        public float maxY = 15f;

        [Header("Скорости")]
        public float moveSpeed = 0.1f;
        public float zoomSpeed = 0.05f;
    }
}