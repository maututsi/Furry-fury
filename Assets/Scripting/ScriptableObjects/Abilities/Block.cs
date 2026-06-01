using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu]
public class Block : MeterAbility
{
    public float damageReductionFactor;
    public EffectData statChangeData;

    public override void Execute(InGameContext userContext)
    {
        StatChangeEffect statChange = new StatChangeEffect(statChangeData, float.MaxValue, CombatStats.ReceivedDamage, damageReductionFactor, userContext.CombatComponent);
        ConditionalEffect cEff = new ConditionalEffect(statChange, (userContext) =>
        {
            Abilities abilityType = userContext.ChosenCharacter.abilitySet.GetAbilityType(this);
            return userContext.CombatController.HoldingAbility[abilityType] &&
            userContext.CooldownManager.LeftCooldowns[abilityType] < userContext.CooldownManager.AbilityCooldowns[abilityType];
        });
        userContext.ConditionalEffectManager.ApplyEffect(cEff);
    }
}