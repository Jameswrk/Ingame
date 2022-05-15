using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public field
    public int hitpoint = 10;
    public int maxHitpoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //immunity
    protected float ImmuneTime = 1.0f;
    protected float lastImmune;

    //push
    protected Vector3 pushDirection;

    //All figther can ReceiveDamage /Die
    protected virtual void ReceiveDamage(Damage dmg)
    {
      if(Time.time - lastImmune > ImmuneTime)
      {
        lastImmune = Time.time;
        hitpoint-= dmg.damageAmount;
        pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
        
        //Show damage
        GameManager.instance.ShowText(dmg.damageAmount.ToString(), 35, Color.red, transform.position, Vector3.zero, 0.5f);
        
        if(hitpoint <=0)
        {
            hitpoint =0;
            Death();
        }
      }

    }

    protected virtual void Death()
    {

    }
}
