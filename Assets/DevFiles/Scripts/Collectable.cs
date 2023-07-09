using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Touchable, ICollectable
{

    public void Collected(Touchable touchable)
    {
        

        Destroy(gameObject);

        
    }


}
