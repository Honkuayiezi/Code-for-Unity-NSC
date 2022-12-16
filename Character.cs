using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float speed;

    [SerializeField] protected float damage;

    protected Animator anim;
    [SerializeField]protected AnimationClip dieClip;
    protected float dieClip_length;

    protected void RegisterCharacter() {
        anim = GetComponentInChildren<Animator>();
        dieClip_length = dieClip.length;

    }

    protected bool Calculate_Damage(float damageReceive) {
        health -= damageReceive;
        if (health <= 0) {
            Debug.Log("Die");
            Die();
            return true;
        }
        return false;
    }

    public virtual void Die() {
        Debug.Log("Die");
        
        anim.SetTrigger("Die");
    }

}
