using System;
using UnityEngine;

namespace LittleLegends.Players
{
    public class CameraController : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private PlayerInputSO inputSo;

        [Header("3인칭 카메라 설정")] [SerializeField]
        private Transform target;

        [SerializeField] private float distance = 5f;
        [SerializeField] private float smoothTime = 0.05f;
        [SerializeField] private float MouseSensitivity = 1.0f;
        [SerializeField] private float minY = -60f;
        [SerializeField] private float maxY = 90;
        private float yaw;
        private float pitch;
        private Vector3 currentVelocity;

        private void Awake()
        {
            inputSo.OnLookEvent += OnLookHandler;
        }

        private void LateUpdate()
        {
            Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
            Vector3 desiredPosition = target.position - (rotation * Vector3.forward * distance);
            Vector3 smoothedPosition =
                Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothTime);
            transform.position = smoothedPosition;
        }

        private void OnLookHandler(Vector2 delta)
        {
            if (delta == Vector2.zero)
                return;
            yaw += delta.x * MouseSensitivity * 0.01f;
            pitch -= delta.y * MouseSensitivity * 0.01f;
            pitch = Mathf.Clamp(pitch, minY, maxY);
        }
    }
}