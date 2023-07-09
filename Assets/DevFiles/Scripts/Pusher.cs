using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour, IPushable
{
    [SerializeField] private int force = 1000;

    public int Force { get => force; set => force = value; }
    public Rigidbody Rb { get; set; }

    public void OnPush(Collision coll, int force)
    {
        //transform.localScale += Vector3.one;
    }

    
}
