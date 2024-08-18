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

    private Coroutine flashingCoroutineRef = null;
    // Start is called before the first frame update
    public void Start(){
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SpriteFlash(float duration, Color flashColor){
        originalColor = spriteRenderer.material.color;
        originalMaterial = spriteRenderer.material;
        if(flashingCoroutineRef != null){
            StopCoroutine(flashingCoroutineRef);
        }

        flashingCoroutineRef = StartCoroutine(FlashCoroutine(duration, flashColor));
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
                }
                
            }else{
                if(flashColor == Color.white){
                    spriteRenderer.material = originalMaterial;
                }else{
                    spriteRenderer.color = originalColor;
                }
            }
            isFlashing = !isFlashing;
            yield return new WaitForSeconds(blinkDuration);
            elapsedTime += blinkDuration;
        }


        spriteRenderer.color = originalColor;
         spriteRenderer.material = originalMaterial;
    }
}
