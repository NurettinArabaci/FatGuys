using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Collectable : Touchable, ICollectable
{
    public void Collected(Touchable touchable)
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
        transform.DOJump(touchable.transform.position,1,1, 0.3f).OnComplete(() => Destroy(gameObject));
        transform.DOScale(Vector3.zero, 0.3f);
        

    }

}
