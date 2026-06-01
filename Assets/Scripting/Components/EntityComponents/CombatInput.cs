using System;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatInput : MonoBehaviour
{
    public CombatInputEvents Events = new CombatInputEvents();
    public Dictionary<Abilities, bool> HoldingAbility;

    public void OnBlock(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.Block);
            HoldingAbility[Abilities.Block] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.Block);
            HoldingAbility[Abilities.Block] = false;
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.LightAttack);
            HoldingAbility[Abilities.LightAttack] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.LightAttack);
            HoldingAbility[Abilities.LightAttack] = false;
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.HeavyAttack);
            HoldingAbility[Abilities.HeavyAttack] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.HeavyAttack);
            HoldingAbility[Abilities.HeavyAttack] = false;
        }
    }

    public void OnAbility1(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.Ability1);
            HoldingAbility[Abilities.Ability1] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.Ability1);
            HoldingAbility[Abilities.Ability1] = false;
        }
    }

    public void OnAbility2(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.Ability2);
            HoldingAbility[Abilities.Ability2] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.Ability2);
            HoldingAbility[Abilities.Ability2] = false;
        }
    }

    public void OnAbility3(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.Ability3);
            HoldingAbility[Abilities.Ability3] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.Ability3);
            HoldingAbility[Abilities.Ability3] = false;
        }
    }

    public void OnAbility4(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Events.NotifyPressed(Abilities.Ability4);
            HoldingAbility[Abilities.Ability4] = true;
        }

        if (context.canceled)
        {
            Events.NotifyReleased(Abilities.Ability4);
            HoldingAbility[Abilities.Ability4] = false;
        }
    }

    private void InitHoldingDihctionary()
    {
        HoldingAbility = new Dictionary<Abilities, bool>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            HoldingAbility[ability] = false;
        }
    }

    void Awake()
    {
        InitHoldingDihctionary();
    }

    void Update()
    {
        
    }
}