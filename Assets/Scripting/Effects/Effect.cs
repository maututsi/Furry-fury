using System;
using System.Collections;
using UnityEngine;

public abstract class Effect
{
    public Coroutine effectInstanceCoroutine;
    public EffectData effectData;

    private float _duration;
    public float Duration
    {
        get => _duration;
        protected set => _duration = value < 0 ? 0 : value;
    }

    public abstract void Apply(EffectManager effectManager);

    public abstract void Remove(EffectManager effectManager);

    public abstract IEnumerator EffectCoroutine(EffectManager effectManager);

    public Effect(EffectData effData, float duration)
    {
        effectData = new EffectData(effData);
        effectInstanceCoroutine = null;
        Duration = duration;
    }
}