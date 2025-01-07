using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GunAttributesSO", menuName = "Gun/GunAttributesSO")]
public class GunAttributesSO : ScriptableObject
{
    public string gunName;
    public int numberBullet, totalBullet, constTotalBullet;
    public float timeReload;
    public bool outOfBullet;
    public float timeShoot;
    public BulletContainerSO bulletContainerSO;
    public bool canShoot = true;

    // The stack contains current bullet number. The list contains used bullets.
    // When the stack is empty, stack will take bullet out of list.
    public Stack<GameObject> bulletStack = new Stack<GameObject>();
    public List<GameObject> listBullet = new List<GameObject>();

    public IntEventChannelSO changeTotalBulletEventSO;
    public GameObject gunModel;
    public float damage;
    public AudioGroupSO shootSoundSO;
    public AudioGroupSO reloadSoundSO;
}

