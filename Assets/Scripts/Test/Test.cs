using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public int num = 10;

    private IEnumerator test(int num){
        while(num > 0){
            yield return new WaitForSeconds(2f);
            num--;
            Debug.Log("" + num);
        }
    }
    private void Start() {
        StartCoroutine(test(num));
    }
}
