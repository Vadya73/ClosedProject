using UnityEngine;

namespace Core.CameraControl
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraConfig _config;
        [SerializeField] private Camera _camera;

        private Vector2 _lastTouchPosition;
        private bool _isDragging;
        [SerializeField] private bool _isActive;
        
        public bool IsActive => _isActive;

        private void Awake()
        {
            if (_camera == null)
                _camera = Camera.main;

            Vector3 rot = _camera.transform.eulerAngles;
            rot.x = 45f;
            rot.z = 0f;
            _camera.transform.eulerAngles = rot;
        }

        private void Update()
        {
            if (!_isActive)
                return;
            
            if (Input.touchCount == 1)
                HandleDrag();
            else if (Input.touchCount == 2)
                HandlePinchZoom();
            else
                _isDragging = false;
        }

        public void SetConfig(CameraConfig config) => _config = config;

        public void SetActiveStateCamera(bool state) => _isActive = state;

        private void HandleDrag()
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                _lastTouchPosition = touch.position;
                _isDragging = true;
            }
            else if (touch.phase == TouchPhase.Moved && _isDragging)
            {
                Vector2 delta = touch.position - _lastTouchPosition;
                _lastTouchPosition = touch.position;

                Vector3 move = new Vector3(-delta.x * _config.moveSpeed * Time.deltaTime, 0, -delta.y * _config.moveSpeed * Time.deltaTime);

                _camera.transform.position += move;
                ClampPosition();
            }
            else if (touch.phase == TouchPhase.Ended)
                _isDragging = false;
        }

        private void HandlePinchZoom()
        {
            Touch t0 = Input.GetTouch(0);
            Touch t1 = Input.GetTouch(1);

            Vector2 prevPos0 = t0.position - t0.deltaPosition;
            Vector2 prevPos1 = t1.position - t1.deltaPosition;

            float prevDist = (prevPos0 - prevPos1).magnitude;
            float currDist = (t0.position - t1.position).magnitude;

            float delta = currDist - prevDist;

            Vector3 pos = _camera.transform.position;
            pos.y -= delta * _config.zoomSpeed * Time.deltaTime;
            pos.y = Mathf.Clamp(pos.y, _config.minY, _config.maxY);

            _camera.transform.position = pos;

            ClampPosition();
        }

        private void ClampPosition()
        {
            Vector3 pos = _camera.transform.position;
            pos.x = Mathf.Clamp(pos.x, _config.minX, _config.maxX);
            pos.z = Mathf.Clamp(pos.z, _config.minZ, _config.maxZ);
            _camera.transform.position = pos;
        }
    }
}