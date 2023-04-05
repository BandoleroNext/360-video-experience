using UnityEngine;

namespace Utils
{
    public class PivotAroundCamera : MonoBehaviour
    {
        private Camera _mainCamera;
        private Transform _transform;

        private void Start()
        {
            _mainCamera = Camera.main;
            _transform = transform;
        }

        private void Update()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
                return;
            }

            _transform.rotation = Quaternion.Lerp(_transform.rotation,
                Quaternion.AngleAxis(_mainCamera.transform.eulerAngles.y, Vector3.up), .01f);
            _transform.position = Vector3.zero;
        }
    }
}