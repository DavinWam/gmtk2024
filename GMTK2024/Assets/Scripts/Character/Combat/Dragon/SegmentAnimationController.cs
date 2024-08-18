using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentAnimationController : MonoBehaviour
{
    private DragonCombatant dragonCombatant;
    private SpriteRenderer spriteRenderer;
    private SpriteEffects spriteEffects;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteEffects = GetComponent<SpriteEffects>();
        animator = GetComponent<Animator>();

        dragonCombatant = GetComponentInParent<DragonCombatant>();
        if(dragonCombatant){
            dragonCombatant.OnDamageTaken += DamageAnim;
            dragonCombatant.OnDeath += DeathAnim;
            dragonCombatant.OnUnlocked += UnlockAnim;
            dragonCombatant.unlock(!dragonCombatant.invincible);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void DamageAnim(float amount){
        spriteEffects.SpriteFlash(dragonCombatant.HitInvinciblityDuration,Color.white,1);
    }
    public void DeathAnim(){
        spriteEffects.StopFlash(1);
        spriteEffects.SetColor(Color.blue);
    }
    public void UnlockAnim(bool unlock){
        Debug.Log("animation");
        if(unlock){
            animator.SetBool("Unlock", true);
        }else{
            animator.SetBool("Unlock", false);
        }
    }
}
