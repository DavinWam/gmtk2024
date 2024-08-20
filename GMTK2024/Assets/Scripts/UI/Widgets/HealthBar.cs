using UnityEngine;
using UnityEngine.UI;

public class HealthBar : UIElement
{
    public Combatant damageableObject;  // Reference to an object that implements IDamageable
    public Slider healthSlider;// Reference to the slider UI element

    void Start()
    {
        damageableObject = GameObject.FindGameObjectWithTag("Player").GetComponent<Combatant>();

        if(damageableObject!= null){
            // Acquire the game layer from the UIManager singleton
            UILayer gameLayer = UIManager.Instance.GetGameLayer();
            if (gameLayer != null)
            {
                gameLayer.Push(this);
            }
            else
            {
                Debug.LogWarning("GameLayer could not be found.");
            }

            // Subscribe to the damage event
            if (damageableObject != null)
            {
                damageableObject.OnDamageTaken += UpdateHealthSlider;
                InitializeHealthSlider();
            }
        }else{
             Debug.LogWarning("couldn't find damage object for health bar " + gameObject.name);
        }


    }

    void OnDestroy()
    {
        // Unsubscribe from the damage event when the health bar is destroyed
        if (damageableObject != null)
        {
            damageableObject.OnDamageTaken -= UpdateHealthSlider;
        }
    }

    private void InitializeHealthSlider()
    {
        // Initialize the slider with the current health of the damageable object
        healthSlider.maxValue = damageableObject.GetStats().maxHealth;
        healthSlider.value = damageableObject.GetStats().currentHealth;
    }

    public void UpdateHealthSlider(float damageAmount)
    {
        // Update the slider UI based on the new health value
        healthSlider.value = damageableObject.GetStats().currentHealth;
    }
}
