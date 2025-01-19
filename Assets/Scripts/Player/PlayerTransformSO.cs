using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerTransformSO", menuName = "Player/PlayerTransformSO")]
public class PlayerTransformSO : ScriptableObject {
    public Vector3 playerPos;
    public void Init(Vector3 initPos) {
        playerPos = initPos;
    }
    
}
