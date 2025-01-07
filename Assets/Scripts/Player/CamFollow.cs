using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReader;

    [Tooltip("How far in degrees can you move the camera up")]
    [SerializeField] private float _topClamp;

    [Tooltip("How far in degrees can you move the camera down")]
    [SerializeField] private float _bottomClamp;
    [SerializeField] private float _mouseSpeed;
    private float _cinemachineTargetYaw;
    private float _cinemachineTargetPitch;
    private const float _moveCamThreshold = 0.01f;


    private void Start()
    {
        _cinemachineTargetYaw = transform.rotation.eulerAngles.y;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnEnable()
    {
        _inputReader.lookAction += OnCameraRotation;
    }

    private void OnCameraRotation(Vector2 mouseInput)
    {
        if (mouseInput.sqrMagnitude >= _moveCamThreshold)
        {
            _cinemachineTargetYaw += mouseInput.x / 50 * _mouseSpeed;
            _cinemachineTargetPitch += mouseInput.y / 100 * _mouseSpeed;
        }
    }

    // private void Update()
    // {
    //     RotateCamera();
    // }

    private void LateUpdate() {
        RotateCamera();
    }
    // private void FixedUpdate()
    // {
    //     RotateCamera();
    // }
    private void RotateCamera()
    {
        _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
        _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, _bottomClamp, _topClamp);
        transform.rotation = Quaternion.Euler(-_cinemachineTargetPitch, _cinemachineTargetYaw, 0.0f);
    }

    private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
    {
        if (lfAngle < -360f) lfAngle += 360f;
        if (lfAngle > 360f) lfAngle -= 360f;
        return Mathf.Clamp(lfAngle, lfMin, lfMax);
    }

    private void OnDisable()
    {
        _inputReader.lookAction -= OnCameraRotation;
    }
}
