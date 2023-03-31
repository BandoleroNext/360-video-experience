using System;
using UnityEngine;

namespace Utils
{
    public class PivotAroundCamera : MonoBehaviour
    {
        private Camera _mainCamera;
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (_mainCamera == null)
            {
                _mainCamera = Camera.main;
                return;
            }
        
            transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.AngleAxis(_mainCamera.transform.eulerAngles.y,Vector3.up),.01f);
            transform.position = Vector3.zero;
        }
    }
}
