using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GunManagerSO", menuName = "Gun/GunManagerSO")]
public class GunManagerSO : ScriptableObject
{
    public List<GunAttributesSO> gunList = new List<GunAttributesSO>();
    public VoidEventChannelSO playerDieEventSO;

    // Used to change UI for gun information. It is subcribed in GunUI class.
    public GunInfoEventChannel gunInfoEventSO;
    
    public int curGun = 0, prevGun = 0;
    public GunAttributesSO GetElemGunAttribute(){
        return gunList[curGun];
    }
    public float curDamage;
    public bool canChangeGun = true;
}

