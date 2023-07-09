using UnityEngine;

public abstract class Touchable : MonoBehaviour,ICollectable
{
    public abstract void OnTouch();

    public virtual void Collected(Touchable touchable) {
        OnTouch();
    }

    public virtual int Force { get; set; }
}
