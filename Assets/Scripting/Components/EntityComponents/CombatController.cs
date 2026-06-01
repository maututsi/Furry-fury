using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CombatController : MonoBehaviour
{
    [SerializeField] private InGameContext playerContext;

    public CombatInput combatInput;
    public Dictionary<Abilities, float> HoldingTimes;
    public Dictionary<Abilities, bool> HoldingAbility => combatInput.HoldingAbility;

    public void OnEnable()
    {
        combatInput.Events.OnPressed += HandleAbility;
        combatInput.Events.OnReleased += HandleAbility;
    }

    public void OnDisable()
    {
        combatInput.Events.OnPressed -= HandleAbility;
        combatInput.Events.OnReleased -= HandleAbility;
    }

    private void InitHoldingTimes()
    {
        HoldingTimes = new Dictionary<Abilities, float>();

        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            HoldingTimes[ability] = 0f;
        }
    }

    private void UpdateHoldingTimes()
    {
        foreach (Abilities ability in Enum.GetValues(typeof(Abilities)))
        {
            if (!combatInput.HoldingAbility[ability])
                HoldingTimes[ability] = 0f;
            else
                HoldingTimes[ability] += Time.deltaTime;
        }
    }

    public void HandleAbility(Abilities ability)
    {
        Ability usedAbility = playerContext.ChosenCharacter.abilitySet.GetAbility(ability);
        CooldownManager userCM = playerContext.CooldownManager;

        if (userCM.ExecutableAbilities[ability])
        {
            if ((!usedAbility.executesWhenPressed && HoldingAbility[ability]) || (usedAbility.executesWhenPressed && !HoldingAbility[ability]))
            {
                userCM.SetLastUseTime(ability);
                usedAbility.Execute(playerContext);
            }
        }
    }

    void Awake()
    {
        playerContext = GetComponent<InGameContext>();
    }

    void Start()
    {
        InitHoldingTimes();
    }

    void Update()
    {
        UpdateHoldingTimes();
    }
}