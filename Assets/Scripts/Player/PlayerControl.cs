using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputReaderSO _inputReaderSO;
    [SerializeField] private PlayerAttributesSO _playerAttributesSO;
    [SerializeField] private Rigidbody _rb;
    [SerializeField] private bool _isGrounded;
    [SerializeField] private Transform _groundCheckPos;
    [SerializeField] private Transform _camPos;
    [SerializeField] private FloatEventChannelSO _changeHPEventSO;
    [SerializeField] private Slider _hpSlider;
    [SerializeField] private VoidEventChannelSO _PlayerDieEventSO;
    private bool _jumpDelay;
    private Transform _camBrain;
    private void Awake() {
        _playerAttributesSO.playerTransformSO.playerTransform = transform;
    }
    private void Start() {
        _jumpDelay = false;
        _camBrain = Camera.main.transform;
    }
    private void OnEnable() {
        _inputReaderSO.moveAction += GetMoveAction;
        _inputReaderSO.jumpAction += Jump;
        _changeHPEventSO.OnRaisedEvent += MinusHP;
        _PlayerDieEventSO.OnRaisedEvent += Die;
        InitHP();
    }
    private void OnDisable() {
        _inputReaderSO.moveAction -= GetMoveAction;
        _inputReaderSO.jumpAction -= Jump;
        _changeHPEventSO.OnRaisedEvent -= MinusHP;
        _PlayerDieEventSO.OnRaisedEvent -= Die;
    }
    private void GetMoveAction(Vector2 dir){
        
        _playerAttributesSO.moveDirection = dir;
    }
    private void Jump(){
        if(!_isGrounded || _playerAttributesSO.isDie) return;
        _isGrounded = false;
        _jumpDelay = false;
        float jumpRealDis = _playerAttributesSO.jumpDistance - transform.position.y;
        float jumpTime = jumpRealDis / _playerAttributesSO.jumpForce;
        _rb.DOMoveY(_playerAttributesSO.jumpDistance, jumpTime).SetEase(Ease.InOutQuad);
        StartCoroutine(WaitJumpDelay(_playerAttributesSO.groundCheckDelay));
    }
    private IEnumerator WaitJumpDelay(float time){
        yield return new WaitForSeconds(time);
        _jumpDelay = true;
    }
    private void CheckGround(){
        RaycastHit raycastHit;
        bool isHit = Physics.Raycast(_groundCheckPos.position, Vector3.down, out raycastHit, _playerAttributesSO.groundCheckDistance, _playerAttributesSO.layerCheckJump);
        Debug.DrawRay(raycastHit.point,Vector3.down * _playerAttributesSO.groundCheckDistance, Color.black);
        if(isHit) {
            _isGrounded = true;
        }
    }
    private void Update() {
        GetMoveDirection();
    }
    private void GetMoveDirection(){
        if(_playerAttributesSO.isDie) return;
        //_playerAttributesSO.playerTransformSO.playerTransform = transform;
        Vector2 movement = _inputReaderSO.GetPlayerPosition();
        Vector3 moveDir = new Vector3(movement.x, 0f, movement.y);
        moveDir = _camBrain.forward * moveDir.z + _camBrain.right * moveDir.x; 
        moveDir.y = 0f;
        _rb.velocity = moveDir * _playerAttributesSO.moveSpeed;
        transform.forward = _camPos.forward;
        if(_jumpDelay) CheckGround();
    }
    private void InitHP(){
        _hpSlider.maxValue = _playerAttributesSO.maxHP;
        _hpSlider.value = _playerAttributesSO.maxHP;
        _playerAttributesSO.curHP = _playerAttributesSO.maxHP;
        _playerAttributesSO.isDie = false;
    }

    private void MinusHP(float amount){
        _playerAttributesSO.curHP -= amount;
        _hpSlider.value = _playerAttributesSO.curHP;
        CheckDie();
    }
    private void CheckDie(){
        if(_playerAttributesSO.curHP <= 0){
            _playerAttributesSO.curHP = 0f;
            _PlayerDieEventSO.RaiseEvent();
        }
    }
    private void Die(){
        _playerAttributesSO.isDie = true;
        Debug.Log("Player Dies");
    }
}
