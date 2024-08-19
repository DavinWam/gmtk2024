using Unity.VisualScripting;
using UnityEngine;

public class AnimationController2D : MonoBehaviour
{
    public float dropWarningPercent = .2f;
    public bool IsWarning = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    private CharacterController2D characterController;
    private PlayerCombatant playerCombatant;
    private SpriteEffects spriteEffects;
    public event System.Action OnFinishedIntroWalk;
 void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteEffects = GetComponent<SpriteEffects>();
        animator = GetComponent<Animator>();

        // Get the CharacterController2D from the parent object
        characterController = GetComponentInParent<CharacterController2D>();
        playerCombatant = GetComponentInParent<PlayerCombatant>();
        
        if(playerCombatant){
            playerCombatant.OnDamageTaken += DamageAnim;
            playerCombatant.OnDeath += DieAnim;
        }
        // Check if characterController is found
        if (characterController == null)
        {
            Debug.LogError("CharacterController2D not found on parent object. Please ensure it is attached to the parent.");
            return;
        }

        // Get the Rigidbody2D from the CharacterController2D
        rb =  GetComponentInParent<Rigidbody2D>();

        // Check if Rigidbody2D is found
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on CharacterController2D. Please ensure it is attached to the parent.");
            return;
        }

        // Check if animator is found
        if (animator == null)
        {
            Debug.LogError("Animator component not found on this object. Please ensure it is attached.");
        }

        // Check if spriteRenderer is found
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on this object. Please ensure it is attached.");
        }

        characterController.OnOutOfStamina += StaminaFlash;
        characterController.OnLatchCooldownEnd += EndStaminaFlash;
    }

    void Update()
    {
        if(!playerCombatant.dead){
            UpdateAnimatorParameters();
            if(!characterController.IsLatching()){
                FlipSprite();
            }
        }
        
        if(characterController.currLatchStamina <= characterController.maxLatchStamina*dropWarningPercent
            && !IsWarning){
                Debug.Log("djkaljdlk");
            IsWarning = true;
            spriteEffects.StopFlash(1);
            spriteRenderer.material.SetColor("_Color", Color.white);
            spriteEffects.SpeedUpFlash(characterController.currLatchStamina/characterController.staminaDrainMultiplier
            ,Color.red,0);
        }
        if( IsWarning && !characterController.IsLatching() && (characterController.IsLatching() || characterController.currLatchStamina > characterController.maxLatchStamina*dropWarningPercent)){
            IsWarning = false;
        }
    }
    public void StaminaFlash(float duration){
        Debug.Log("stamina out");
        if(characterController != null){
            spriteEffects.StopFlash(1);
            spriteRenderer.material.SetColor("_Color", Color.white);
            spriteEffects.SpriteFlash(duration, new Color(1,0.6132074f,0.6132074f),0);
        }  
    }
    public void EndStaminaFlash(){
        Debug.Log("can grab again");
        if(characterController != null){
            spriteRenderer.material.SetColor("_Color", Color.white);
            spriteEffects.StopFlash(0);
        }  
    }
    public void Attack(){
            if (characterController.IsLatching()){
                 animator.SetTrigger("Attack2");
            }else{
                animator.SetTrigger("Attack");
            }           
    }
    public void DamageAnim(float amount){
        animator.SetTrigger("Hit");
        spriteEffects.SpriteFlash(playerCombatant.HitInvinciblityDuration,Color.white,1);
    }
    public void DieAnim(){
        animator.SetTrigger("Death");
        animator.SetBool("IsDead", true);
    }
    void FlipSprite()
    {
        float moveInput = rb.velocity.x;

        if (moveInput > 0.05)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            //spriteRenderer.flipX = false; // Facing right
        }
        else if (moveInput < -0.05)
        {
            //spriteRenderer.flipX = true; // Facing left
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    void UpdateAnimatorParameters()
    {
        bool isGrounded = characterController.IsGrounded(); // Assuming IsGrounded is a method in CharacterController2D
        bool isLatching = characterController.IsLatching();

        if (isLatching)
        {
            animator.SetBool("IsLatching", true);
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsJumping", false);
        }else{
             animator.SetBool("IsLatching", false);
            // Check if character is moving horizontally and grounded
            if (Mathf.Abs(rb.velocity.x) > 0.1f && isGrounded)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }

            // Check if the character is not grounded
            if (!isGrounded)
            {
                animator.SetBool("IsJumping", true);
            }
            else
            {
                animator.SetBool("IsJumping", false);
            }
        }

    }
    public void TriggerFinishedWalk(){
        OnFinishedIntroWalk?.Invoke();
    }
}
