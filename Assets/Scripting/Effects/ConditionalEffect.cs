using System;
using System.Collections;
using UnityEngine;

public class ConditionalEffect
{
    public Effect effect;
    public Predicate<InGameContext> condition;

    public ConditionalEffect(Effect applyingEffect, Predicate<InGameContext> whilst)
    {
        effect = applyingEffect;
        condition = whilst;
    }
}