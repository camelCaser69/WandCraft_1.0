// ==================== ModifierBase.cs ====================
// Base class for spell and wand modifiers
using UnityEngine;

public abstract class ModifierBase : ScriptableObject
{
    public string modifierName;
    public abstract void ApplyModifier(SpellBase spell);
}