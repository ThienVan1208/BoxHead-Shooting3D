using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityScale : MonoBehaviour
{
   [SerializeField] private Rigidbody _rb;
   [SerializeField] private float _gravityScale;
   private void FixedUpdate() {
    _rb.AddForce(Vector3.down * _gravityScale);
   }
}
