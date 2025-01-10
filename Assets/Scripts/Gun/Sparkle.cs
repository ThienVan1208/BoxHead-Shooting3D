using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sparkle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _spark;
    public void ActiveSpark(){
        _spark.Play();
    }
}
