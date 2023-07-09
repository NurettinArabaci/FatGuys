using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Touchable
{

    public override void Collected(Touchable touchable)
    {
        
        touchable.Force += 100;

        touchable.GetComponent<Rigidbody>().mass += 0.2f;
        touchable.transform.localScale += Vector3.one / 5;

        Destroy(gameObject);

        
    }

    public override void OnTouch()
    {

    }

}
