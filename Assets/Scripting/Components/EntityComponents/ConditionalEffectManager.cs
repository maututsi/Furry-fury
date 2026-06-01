using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionalEffectManager : MonoBehaviour
{
    public EffectManager effectManager;
    public List<ConditionalEffect> conditionalEffectStack;

    private InGameContext userContext;

    public void ApplyEffect(ConditionalEffect effect)
    {
        conditionalEffectStack.Add(effect);
        effectManager.ApplyEffect(effect.effect);
    }

    public void RemoveEffect(ConditionalEffect effect)
    {
        conditionalEffectStack.Remove(effect);
        effectManager.CleanEffect(effect.effect);
    }

    public void CheckConditionalEffects(InGameContext userCtx)
    {
        List<ConditionalEffect> copy = new List<ConditionalEffect>(conditionalEffectStack);

        foreach (ConditionalEffect effect in copy)
        {
            if (!effect.condition(userCtx))
            {
                RemoveEffect(effect);
            }
        }
    }

    void Awake()
    {
        conditionalEffectStack = new List<ConditionalEffect>();
        userContext = GetComponent<InGameContext>();
        effectManager = GetComponent<EffectManager>();
    }

    void Update()
    {
        CheckConditionalEffects(userContext);
    }
}