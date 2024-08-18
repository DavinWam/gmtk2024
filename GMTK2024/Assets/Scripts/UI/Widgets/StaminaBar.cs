using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : UIElement
{
    public CharacterController2D character;  // Reference to an object that implements IDamageable
    public Slider staminaSlider;           // Reference to the slider UI element

    void Start()
    {
        if(character != null){
            // Acquire the game layer from the UIManager singleton
            UILayer gameLayer = UIManager.Instance.GetGameLayer();
            if (gameLayer != null)
            {
                gameLayer.Push(this);
                staminaSlider.maxValue = character.maxLatchStamina;
                staminaSlider.value = character.currLatchStamina;
            }
            else
            {
                Debug.LogWarning("GameLayer could not be found.");
            }
        }else{
             Debug.LogWarning("couldn't find damage object for health bar " + gameObject.name);
        }


    }

    void Update() {
        staminaSlider.value = character.currLatchStamina;
    }

}
