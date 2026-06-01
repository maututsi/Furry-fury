using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class EffectData : ScriptableObject
{
    public int effectId;
    public string effectName;
    public string effectMessage;
    public bool showsMessage;
    public bool canStack;
    public bool isNegative;

    public EffectData(EffectData effData)
    {
        effectId = effData.effectId;
        effectName = effData.effectName;
        effectMessage = effData.effectMessage;
        showsMessage = effData.showsMessage;
        canStack = effData.canStack;
        isNegative = effData.isNegative;
    }
}