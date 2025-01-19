using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemContainer : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    private float _timeActiveItem = 60f;

    private IEnumerator WaitActiveItem(float time){
        yield return new WaitForSeconds(time);
        _item.SetActive(true);
    }
    public void GetActiveItem(){
        StartCoroutine(WaitActiveItem(_timeActiveItem));
    }
}
