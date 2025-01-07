using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Camera/CameraAttributeSO", fileName = "CameraAttributesSO")]
public class CameraAttributesSO : ScriptableObject
{
    public float mouseSensitive;
    public float clampAngle;
    public Vector3 rotation;
}
