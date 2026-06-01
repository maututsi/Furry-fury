using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public enum Abilities
{
    LightAttack = 0,
    HeavyAttack = 1,
    Ability1 = 2,
    Ability2 = 3,
    Ability3 = 4,
    Ability4 = 5,
    Block = 6
}

public class CooldownManager : MonoBehaviour
{
    public CooldownsEvents Events { get; private set; } = new CooldownsEvents();

    [SerializeField] private AbilitySet abilitySet;
    [SerializeField] private CombatController controller;

    /*
        ======================
            Cooldown times
        ======================
    */
    private Dictionary<Abilities, float> _abilityCooldowns;
    public Dictionary<Abilities, float> AbilityCooldowns
    {
        get => _abilityCooldowns;
        private set
        {
            if (value is null)
                throw new NullReferenceException("Ability cooldowns must not be null.");

            _abilityCooldowns = value;
        }
    }

    /*
        ===========================
            Left cooldown times
        ===========================
    */
    private Dictionary<Abilities, float> _leftCooldowns;
    public Dictionary<Abilities, float> LeftCooldowns
    {
        get => _leftCooldowns;
        private set
        {
            if (value is null)
                throw new NullReferenceException("Left cooldowns must not be null.");

            _leftCooldowns = value;
        }
    }

    /*
        =====================
            Trigger times
        =====================
    */
    private Dictionary<Abilities, float> _lastUseTimes;
    private Dictionary<Abilities, float> LastUseTimes
    {
        get => _lastUseTimes;
        set
        {
            if (value is null)
                throw new NullReferenceException("Last use times must not be null.");

            _lastUseTimes = value;
        }
    }

    /*
        ==========================
            Available abilities
        ==========================
    */

    public Dictionary<Abilities, bool> _executableAbilities;
    public Dictionary<Abilities, bool> ExecutableAbilities
    {
        get => _executableAbilities;
        set
        {
            if (value is null)
                throw new NullReferenceException("Executable abilities must not be null.");

            _executableAbilities = value;
        }
    }

    /*
    =======================
        Initializations
    =======================
    */
    private void InitAbilityCooldowns()
    {
        AbilityCooldowns = new Dictionary<Abilities, float>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            Ability currentAbility = abilitySet.GetAbility(ability);

            if (currentAbility is null) continue;

            AbilityCooldowns[ability] = currentAbility.cooldown;
        }
    }

    private void InitLastUseTimes()
    {
        LastUseTimes = new Dictionary<Abilities, float>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            LastUseTimes.Add(ability, -1f);
        }
    }

    private void InitLeftCooldowns()
    {
        LeftCooldowns = new Dictionary<Abilities, float>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            LeftCooldowns[ability] = LeftCooldown(ability, -1);
        }
    }

    private void InitExecutableAbilities()
    {
        ExecutableAbilities = new Dictionary<Abilities, bool>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            ExecutableAbilities[ability] = true;
        }
    }

    /*
        ===========================
            Left cooldown logic
        ===========================
    */

    private float LeftCooldown(Abilities ability, float lastUseTime)
    {
        Ability currentAbility = abilitySet.GetAbility(ability);

        if (lastUseTime == -1f) return 0f;

        float abilityCooldown = currentAbility.cooldown;

        if (currentAbility is MeterAbility)
        {
            if (controller.HoldingAbility[ability])
                return Mathf.Clamp(LeftCooldowns[ability] + Time.deltaTime, 0f, abilityCooldown);
            else
                return Mathf.Clamp(LeftCooldowns[ability] - Time.deltaTime, 0f, abilityCooldown);
        }

        float secondsSinceLastUse = Time.time - LastUseTimes[ability];
        return Mathf.Clamp(abilityCooldown - secondsSinceLastUse, 0f, abilityCooldown);
    }

    private void CalculateLeftCooldowns()
    {
        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            float newCooldown = LeftCooldown(ability, LastUseTimes[ability]);

            if (LeftCooldowns[ability] != newCooldown)
            {
                if (newCooldown == 0)
                    Events.NotifyCooldownFinished();

                LeftCooldowns[ability] = newCooldown;
                Events.NotifyCooldownChange();
            }
        }
    }

    public void UpdateExecutableAbilities()
    {
        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            Ability currentAbility = abilitySet.GetAbility(ability);
            MeterAbility meterCurrentAbility = currentAbility as MeterAbility;

            if (meterCurrentAbility == null)
            {
                ExecutableAbilities[ability] = LeftCooldowns[ability] == 0f;
            }
            else
            {
                ExecutableAbilities[ability] = LeftCooldowns[ability] < meterCurrentAbility.cooldown * meterCurrentAbility.minimumChargeForExecution;
            }
        }
    }

    public void SetLastUseTime(Abilities ability) => LastUseTimes[ability] = Time.time;

    void Awake()
    {
        controller = GetComponent<CombatController>();
    }

    void Start()
    {
        abilitySet = GetComponent<InGameContext>().ChosenCharacter.abilitySet;
        InitLastUseTimes();
        InitLeftCooldowns();
        InitAbilityCooldowns();
        InitExecutableAbilities();
    }

    void Update()
    {
        CalculateLeftCooldowns();
        UpdateExecutableAbilities();
    }
}