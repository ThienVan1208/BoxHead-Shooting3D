using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerTransformSO", menuName = "Player/PlayerTransformSO")]
public class PlayerTransformSO : ScriptableObject {
    public Transform playerTransform;
    public void Init(Transform initTransform) {
        playerTransform = initTransform;
    }
}
