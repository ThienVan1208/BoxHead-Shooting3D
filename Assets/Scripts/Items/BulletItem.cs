using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletItem : ItemBase
{
    [SerializeField] private GunManagerSO _gunManagerSO;
    protected override void GetItemEffect()
    {
        foreach (var gun in _gunManagerSO.gunList){
            gun.totalBullet = gun.constTotalBullet;
            _gunManagerSO.gunInfoEventSO.RaiseEvent(_gunManagerSO.gunList[_gunManagerSO.curGun].gunName
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].numberBullet
                                , _gunManagerSO.gunList[_gunManagerSO.curGun].totalBullet);
        }
    }

}
