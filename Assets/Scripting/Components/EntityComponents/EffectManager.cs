using System;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
    private List<Effect> effectStack = new List<Effect>();

    public void ApplyEffect(Effect effect)
    {
        if (!effect.effectData.canStack)
        {
            RemoveEffectsById(effect.effectData.effectId);
        }

        effectStack.Add(effect);

        effect.Apply(this);
    }

    public void CleanEffect(Effect effect)
    {
        if (!effectStack.Contains(effect))
            return;

        effect.Remove(this);
        effectStack.Remove(effect);
    }

    public void CleanseEffectsByType(Type effectType)
    {
        List<Effect> copy = new List<Effect>(effectStack);

        foreach (Effect effect in copy)
        {
            if (effect.GetType() != effectType)
                continue;

            effect.Remove(this);
            effectStack.Remove(effect);
        }
    }

    public void CleanseAllNegativeEffects()
    {
        List<Effect> copy = new List<Effect>(effectStack);

        foreach (Effect effect in copy)
        {
            if (!effect.effectData.isNegative)
                continue;

            effect.Remove(this);
            effectStack.Remove(effect);
        }
    }

    public void CleanseAllPositiveEffects()
    {
        List<Effect> copy = new List<Effect>(effectStack);

        foreach (Effect effect in copy)
        {
            if (effect.effectData.isNegative)
                continue;

            effect.Remove(this);
            effectStack.Remove(effect);
        }
    }

    public void Cleanse()
    {
        List<Effect> copy = new List<Effect>(effectStack);

        foreach (Effect effect in copy)
        {
            effect.Remove(this);
            effectStack.Remove(effect);
        }
    }

    private void RemoveEffectsById(int effectId)
    {
        var effectsToRemove = effectStack.FindAll(e => e.effectData.effectId == effectId);

        foreach (Effect e in effectsToRemove)
        {
            CleanEffect(e);
        }
    }
}