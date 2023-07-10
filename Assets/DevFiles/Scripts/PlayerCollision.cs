using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerCollision : Touchable, IPushable
{
    [SerializeField] private int force = 1000;

    public override int Force { get => force; set => force = value; }

    [HideInInspector]
    public PlayerCollision collidePlayer;
    public Transform mouthPos;


    private void OnCollisionEnter(Collision coll)
    {
        if (coll.transform.TryGetComponent(out ICollectable collectable))
        {
            collectable.Collected(this);
            OnCollected();
        }

        if(coll.transform.TryGetComponent(out IPushable pusher))
        {
            OnPush(coll, pusher.Force);


            if (coll.transform.GetComponent<PlayerCollision>())
            {
                collidePlayer = coll.transform.GetComponent<PlayerCollision>();
                return;
            }

            pusher.OnPush(coll, pusher.Force);
        }

            

    }

    void OnCollected()
    {
        GrowUp(1);
    }


    Vector3 PushPoint(Collision coll)
    {
        Vector3 pos = coll.transform.position;
        Vector3 _point = coll.GetContact(0).point;
        return new Vector3(_point.x - pos.x, 0, _point.z - pos.z);
    }

    public void AddScore(int force)
    {
        GrowUp(force / 200);
    }

    void GrowUp(int multiply)
    {
        Rb.mass += 0.1f * multiply;
        transform.DOScale(transform.localScale + Vector3.one / 5 * multiply, 0.2f).SetEase(Ease.InOutBounce);
        Force += 200 * multiply;
    }

    public void OnPush(Collision coll,int force)
    {
        Rb.AddForce(PushPoint(coll) * force);
    }

    public override void OnDie()
    {
        if(GetComponent<PlayerController>().IsMine)
        {
            GameStateEvent.Fire_OnChangeGameState(GameState.Lose);
        }

        if (collidePlayer)
        {
            collidePlayer.AddScore(Force / 4); ;
        }

        Destroy(gameObject, 1);
    }
}
