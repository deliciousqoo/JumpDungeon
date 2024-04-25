using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public Rigidbody2D rigid;
    public Animator anim;
    public SpriteRenderer spriteRenderer;
    public CompositeCollider2D composite;

}

public interface IApplyForce
{
    void AddForceCall(GameObject player);
    IEnumerator AddForce(GameObject player);
}
public interface IAffectPlayer
{
    PlayerAffectedValue playerData { get; set; }
    void InitAffectValue();
}

public interface IPlayEffect
{
    IEnumerator PlayerEffect();
}