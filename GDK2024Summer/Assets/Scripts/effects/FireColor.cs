using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireColor : MonoBehaviour
{

    public static Color GetComplementaryColor(Color original)
    {
        float hue, saturation, value;
        Color.RGBToHSV(original, out hue, out saturation, out value);

        // Adjust the hue to get the complementary color.
        hue += 0.35f;
        if (hue > 1f) hue -= 1f;

        return Color.HSVToRGB(hue, saturation, value);
    }

    public static Color GetFireTypeColor(float desiredIntensity = 1f, FireType fireType= FireType.None)
    {
        Color rawColor;

        switch(fireType)
        {
            case FireType.STORM:
                rawColor = Color.red;
                break;
            case FireType.LIGHTNING:
                rawColor = new Color(148f/255f, 0f/255f, 211f/255f);  // purple
                break;
            case FireType.SUN:
                rawColor = Color.yellow;
                break;
            case FireType.RAIN:
                rawColor = Color.cyan;
                break;
            case FireType.SNOW:
                rawColor = Color.white;
                break;
            case FireType.CLOUD:
                rawColor = Color.green;
                break;
            case FireType.SKY:
                rawColor = new Color(255f/255f, 165f/255f, 0f/255f);  // orange       
                break;
            default:
                rawColor = Color.black;
                break;
        }

        // Calculate the average intensity of the raw color.
        float currentIntensity = (rawColor.r + rawColor.g + rawColor.b) / 3f;

        // Compute the factor to normalize the color's intensity.
        float factor = desiredIntensity / currentIntensity;

        // Apply the factor to adjust the intensity.
        return new Color(rawColor.r * factor, rawColor.g * factor, rawColor.b * factor, rawColor.a);
    }
}
