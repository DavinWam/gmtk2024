using UnityEngine;
using System.Collections.Generic;
using System;


public class Act
{
    public ActionType actionType;
    public CombatCharacter target; // Stores the target character of the action
    public Spell spell;
    public bool isCritical = false;
    public bool isBlock = false;
    public bool isWeak = false;
    public bool IsFinished { get; private set; } = false;

    // Use this method to mark an action as finished when it's done.
    public void MarkAsFinished()
    {
        IsFinished = true;
    }
    public Act Copy(Act other)
    {
        Act newAct = new Act();
        newAct.actionType = other.actionType;
        newAct.target = other.target;
        newAct.spell = other.spell;
        newAct.isCritical = other.isCritical;
        newAct.isBlock = other.isBlock;
        newAct.isWeak = other.isWeak;
        return newAct;
        // Copy any other relevant properties
    }
    // This implicit operator allows you to directly assign an ActionType to an Act.
    // However, be careful with this as it could introduce bugs if used incorrectly.
    // Especially, this doesn't assign a target, which may or may not be problematic.
    public static implicit operator Act(ActionType v)
    {
        return new Act { actionType = v };  // This will set the actionType, but leave the target as null
    }

    // Additional methods or properties can be added as needed
}