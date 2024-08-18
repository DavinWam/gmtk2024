using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteEffects : MonoBehaviour
{
    public float blinkDuration = .2f;
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private Material originalMaterial;
    public Material spriteWhite;
    private int currentPriority = int.MinValue; // Lowest possible priority

    private Coroutine flashingCoroutineRef = null;
    // Start is called before the first frame update
    public void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.material;
    }
    //for the children
  // Stop the current flash effect if the new one has equal or higher priority
    public void StopFlash(int priority)
    {
        if (priority >= currentPriority)
        {
            if (flashingCoroutineRef != null)
            {
                StopCoroutine(flashingCoroutineRef);
                flashingCoroutineRef = null;
                currentPriority = int.MinValue; // Reset priority
            }
        }
    }

    public void SpriteFlash(float duration, Color flashColor, int priority)
    {
        if (priority >= currentPriority)
        {
            currentPriority = priority;
            if (flashingCoroutineRef != null)
            {
                StopCoroutine(flashingCoroutineRef);
            }
            originalColor = spriteRenderer.color;   
            flashingCoroutineRef = StartCoroutine(FlashCoroutine(duration, flashColor));
        }
    }

    public void SpeedUpFlash(float duration, Color flashColor, int priority)
    {
        if (priority >= currentPriority)
        {
            currentPriority = priority;
            if (flashingCoroutineRef != null)
            {
                StopCoroutine(flashingCoroutineRef);
            }
            originalColor = spriteRenderer.color;
            flashingCoroutineRef = StartCoroutine(FlashSpeedCoroutine(duration, flashColor));
        }
    }

    private IEnumerator FlashCoroutine(float duration, Color flashColor){
        float elapsedTime = 0f;
        bool isFlashing = true;
        while(elapsedTime< duration){
            if(isFlashing){
                if(flashColor == Color.white){
                    spriteRenderer.material = spriteWhite;
                }else{
                    spriteRenderer.color = flashColor;
                    spriteRenderer.material.SetColor("_Color",flashColor);
                }
                
            }else{
                if(flashColor == Color.white){
                    spriteRenderer.material = originalMaterial;
                }else{
                    spriteRenderer.color = originalColor;
                    spriteRenderer.material.SetColor("_Color",originalColor);
                }
            }
            isFlashing = !isFlashing;
            yield return new WaitForSeconds(blinkDuration);
            elapsedTime += blinkDuration;
        }


        spriteRenderer.color = originalColor;
         spriteRenderer.material = originalMaterial;
         spriteRenderer.material.SetColor("_Color",originalColor);
    }
    private IEnumerator FlashSpeedCoroutine(float duration, Color flashColor){
        float elapsedTime = 0f;
        bool isFlashing = true;
        float speedBlinkDuration = blinkDuration;
        while(elapsedTime< duration){
            if(isFlashing){
                if(flashColor == Color.white){
                    spriteRenderer.material = spriteWhite;
                }else{
                    spriteRenderer.color = flashColor;
                    spriteRenderer.material.SetColor("_Color",flashColor);
                }
                
            }else{
                if(flashColor == Color.white){
                    spriteRenderer.material = originalMaterial;
                }else{
                    spriteRenderer.color = originalColor;
                    spriteRenderer.material.SetColor("_Color",originalColor);
                }
            }
            isFlashing = !isFlashing;
            yield return new WaitForSeconds(speedBlinkDuration);
            elapsedTime += speedBlinkDuration;
            speedBlinkDuration = Mathf.Max(.1f, speedBlinkDuration*.9f);
            
        }


         spriteRenderer.color = originalColor;
         spriteRenderer.material = originalMaterial;
         spriteRenderer.material.SetColor("_Color",originalColor);
    }
}
