using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Pusher : MonoBehaviour, IPushable
{
    [SerializeField] private int force = 1000;

    public int Force { get => force; set => force = value; }
    public Rigidbody Rb { get; set; }

    public void OnPush(Collision coll, int force)
    {
        transform.DOScale(transform.localScale * 1.2f, 0.1f).OnComplete(() =>
            transform.DOScale(transform.localScale / 1.2f, 0.1f));
    }

    
}
