using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AbilitySet : ScriptableObject
{
    public Ability lightAttack;
    public Ability heavyAttack;

    public Ability ability1;
    public Ability ability2;
    public Ability ability3;
    public Ability ability4;

    public Ability block;

    public Ability GetAbility(Abilities ability)
    {
        Ability result;

        switch (ability)
        {
            case Abilities.LightAttack:
                result = lightAttack;
                break;
            case Abilities.HeavyAttack:
                result = heavyAttack;
                break;
            case Abilities.Ability1:
                result = ability1;
                break;
            case Abilities.Ability2:
                result = ability2;
                break;
            case Abilities.Ability3:
                result = ability3;
                break;
            case Abilities.Ability4:
                result = ability4;
                break;
            case Abilities.Block:
                result = block;
                break;
            default:
                result = null;
                break;
        }

        return result;
    }

    public Abilities GetAbilityType(Ability ability)
    {
        if (ability.id == block.id) return Abilities.Block;
        else if (ability.id == lightAttack.id) return Abilities.LightAttack;
        else if (ability.id == heavyAttack.id) return Abilities.HeavyAttack;
        else if (ability.id == ability1.id) return Abilities.Ability1;
        else if (ability.id == ability2.id) return Abilities.Ability2;
        else if (ability.id == ability3.id) return Abilities.Ability3;
        else return Abilities.Ability4;
    }
}
