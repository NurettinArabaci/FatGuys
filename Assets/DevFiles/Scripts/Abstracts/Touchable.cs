using System;
using UnityEngine;

public abstract class Touchable : MonoBehaviour
{
    public virtual int Force { get; set; }

    private Rigidbody _rb;
    public virtual Rigidbody Rb { get => _rb; set => _rb = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public virtual void OnDie(){}
}
