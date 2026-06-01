using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Health health;
    public Slider healthBar;
    public TextMeshProUGUI healthPercentageTMP;

    public void OnEnable()
    {
        health.Events.OnDamaged += UpdateHealthBar;
        health.Events.OnHealed += UpdateHealthBar;

        health.Events.OnDamaged += UpdateHealthPercentage;
        health.Events.OnHealed += UpdateHealthPercentage;

        health.Events.OnRevive += RestartHealthBar;
        health.Events.OnRevive += RestartPercentage;
    }

    public void OnDisable()
    {
        health.Events.OnDamaged -= UpdateHealthBar;
        health.Events.OnHealed -= UpdateHealthBar;

        health.Events.OnDamaged -= UpdateHealthPercentage;
        health.Events.OnHealed -= UpdateHealthPercentage;

        health.Events.OnRevive -= RestartHealthBar;
        health.Events.OnRevive -= RestartPercentage;
    }

    public void UpdateHealthBar(float updatedHealth, float maxHealth)
    {
        healthBar.value = updatedHealth / maxHealth;
    }

    public void UpdateHealthPercentage(float updatedHealth, float maxHealth)
    {
        int percentage = Mathf.RoundToInt(updatedHealth * 100f / maxHealth);

        healthPercentageTMP.text = percentage.ToString() + "%";
    }

    private void RestartHealthBar()
    {
        if (health.MaxHealth == 0)
            healthBar.value = 1;
        else
            healthBar.value = health.HealthPercentage;
    }

    private void RestartPercentage()
    {
        if (health.MaxHealth == 0)
            healthPercentageTMP.text = "100%";
        else
        {
            string percentage = Mathf.RoundToInt(health.HealthPercentage * 100f).ToString() + "%";
            healthPercentageTMP.text = percentage;
        }
    }

    void Start()
    {
        RestartHealthBar();
        RestartPercentage();
    }
}
