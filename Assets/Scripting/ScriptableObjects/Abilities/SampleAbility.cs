using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu]
public class SampleAbility : MeterAbility
{
    public EffectData dotData;

    public override void Execute(InGameContext userContext)
    {
        DOTEffect dot = new DOTEffect(dotData, float.MaxValue, 3f, 0.3f, userContext.CombatComponent);
        ConditionalEffect cEff = new ConditionalEffect(dot, (userContext) =>
        {
            Abilities abilityType = userContext.ChosenCharacter.abilitySet.GetAbilityType(this);
            Debug.Log(userContext.CombatController.HoldingAbility[abilityType] && userContext.CooldownManager.LeftCooldowns[abilityType] < cooldown * minimumChargeForExecution);
            return userContext.CombatController.HoldingAbility[abilityType] && userContext.CooldownManager.LeftCooldowns[abilityType] < cooldown * minimumChargeForExecution;
        });
        userContext.ConditionalEffectManager.ApplyEffect(cEff);
    }
}
