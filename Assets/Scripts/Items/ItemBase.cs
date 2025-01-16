using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    //[SerializeField] protected ItemSO _itemSO;
    protected float disMove = 1, timeMove = 3, rotateAngle = 359;
    protected Sequence moveSe, rotateSe;

    protected virtual void OnEnable(){
        MoveY();
        RotateY();
    }
    protected virtual void OnDisable(){
        StopAction();
    }
    protected abstract void GetItemEffect();
    protected virtual void MoveY(){
        moveSe = DOTween.Sequence();
        moveSe.Append(transform.DOMoveY(transform.position.y + disMove, timeMove).SetEase(Ease.InOutQuad))
        .Append(transform.DOMoveY(transform.position.y - disMove, timeMove).SetEase(Ease.InOutQuad))
        .SetLoops(-1, LoopType.Yoyo);
    }
    protected virtual void RotateY(){
        rotateSe = DOTween.Sequence();
        rotateSe.Append(transform.DORotate(new Vector3(0, transform.eulerAngles.y + rotateAngle, 0), timeMove, RotateMode.FastBeyond360))
        .SetLoops(-1, LoopType.Yoyo);
    }
    protected virtual void StopAction(){
        moveSe.Kill();
        rotateSe.Kill();
    }
    protected virtual void OnTriggerEnter(Collider other) {
        if(other.gameObject.layer == Constant.LAYER_PLAYER){
            GetItemEffect();
            gameObject.SetActive(false);
        }
    }
}
