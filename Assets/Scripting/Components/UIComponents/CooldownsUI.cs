using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CooldownsUI : MonoBehaviour
{
    public CooldownManager cooldownManager;
    public Image lightAttackIcon;
    public Image heavyAttackIcon;
    public Image ability1Icon;
    public Image ability2Icon;
    public Image ability3Icon;
    public Image ability4Icon;
    public Image blockIcon;

    private Dictionary<Abilities, Image> AbilityIcons;

    public void OnEnable()
    {
        cooldownManager.Events.OnCooldownChange += UpdateCooldowns;
        cooldownManager.Events.OnCooldownFinished += PlayCooldownFinishedVFX;
    }

    public void OnDisable()
    {
        cooldownManager.Events.OnCooldownChange -= UpdateCooldowns;
        cooldownManager.Events.OnCooldownFinished -= PlayCooldownFinishedVFX;
    }

    private void InitAbilityIconsDictionary()
    {
        AbilityIcons = new Dictionary<Abilities, Image>();

        AbilityIcons[Abilities.LightAttack] = lightAttackIcon;
        AbilityIcons[Abilities.HeavyAttack] = heavyAttackIcon;
        AbilityIcons[Abilities.Ability1] = ability1Icon;
        AbilityIcons[Abilities.Ability2] = ability2Icon;
        AbilityIcons[Abilities.Ability3] = ability3Icon;
        AbilityIcons[Abilities.Ability4] = ability4Icon;
        AbilityIcons[Abilities.Block] = blockIcon;
    }

    private void UpdateCooldowns()
    {
        Dictionary<Abilities, float> abilityCooldowns = cooldownManager.AbilityCooldowns;

        Dictionary<Abilities, float> leftCooldowns = cooldownManager.LeftCooldowns;

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            if (abilityCooldowns[ability] == 0) continue;

            AbilityIcons[ability].fillAmount = Mathf.Clamp01(leftCooldowns[ability] / abilityCooldowns[ability]);
        }
    }

    private void ResetCooldownIcons()
    {
        foreach (Image abilityIcon in AbilityIcons.Values)
        {
            abilityIcon.fillAmount = 0f;
        }
    }

    private void PlayCooldownFinishedVFX()
    {
        Debug.Log("Cooldown ready");
    }

    void Start()
    {
        InitAbilityIconsDictionary();
        ResetCooldownIcons();
    }
}