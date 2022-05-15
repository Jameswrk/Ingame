using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    //Damage struct
    public int[] damagePoint ={ 1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = { 2.0f, 2.2f, 2.5f, 3f, 3.2f, 3.6f, 4f}; //ทำให้กระเด็น

    //upgrade อัปเกรตอาวุธ
    public int weaponLevel = 0;
    public SpriteRenderer spriteRenderer;

    //Swing เอาไว้ตรวจสอบว่าจะสามารถฟันได้หรือยัง
    private Animator anim;
    private float cooldown = 0.5f;
    private float lastSwing;
   
    
    protected override void Start() 
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }
    
    protected override void OnCollide(Collider2D coll)
    {
       if(coll.tag == "Fighter")
       {
           if(coll.name == "Player")
              return;
        
        //Create a new damage object, then we'll sent it to fighter we've hit
        Damage dmg = new Damage
        {
            damageAmount = damagePoint[weaponLevel],
            origin = transform.position,
            pushForce = pushForce[weaponLevel]
        };

        coll.SendMessage("ReceiveDamage", dmg);
           
       }
       
    }

    private void Swing()
    {
        anim.SetTrigger("Swing");
    }
    
    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];

        //change stats %%
    }

    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
