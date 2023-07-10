using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if(other.TryGetComponent(out Touchable touchable))
        {
            touchable.OnDie();
        }
    }
}
