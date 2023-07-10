using UnityEngine;
public interface IPushable
{
    public void OnPush(Collision coll, int force);

    public Rigidbody Rb { get; set; }

    public int Force { get; set; }
}
