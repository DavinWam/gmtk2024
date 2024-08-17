using UnityEditor;
using UnityEngine;

public class PlayerCombatantTool : EditorWindow
{
    private PlayerCombatant selectedCombatant;

    [MenuItem("Tools/Player Combatant Tool")]
    public static void ShowWindow()
    {
        GetWindow<PlayerCombatantTool>("Player Combatant Tool");
    }
 
    private void OnGUI()
    {
        GUILayout.Label("Player Combatant Tool", EditorStyles.boldLabel);

        selectedCombatant = (PlayerCombatant)EditorGUILayout.ObjectField("Selected Combatant", selectedCombatant, typeof(PlayerCombatant), false);

        if (selectedCombatant != null)
        {
            EditorGUILayout.LabelField("Character Stats:");
            EditorGUILayout.ObjectField("Character Stats", selectedCombatant.characterStats, typeof(PlayerStats), false);

            if (GUILayout.Button("Toggle Wings"))
            {
                selectedCombatant.ToggleWings(!selectedCombatant.wings.activeSelf);
            }

            // Add more controls for other functionalities
        }
        else
        {
            EditorGUILayout.HelpBox("Select a Player Combatant to begin editing.", MessageType.Info);
        }
    }
}
