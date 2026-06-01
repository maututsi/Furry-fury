using System.Collections;
using TMPro;
using UnityEngine;

public class PlayerInfoUI: MonoBehaviour
{
    public PlayerData playerData;
    public TextMeshProUGUI chosenCatTMP;
    public TextMeshProUGUI playerNameTMP;
    
    private void InitializeChosenCatName()
    {
        chosenCatTMP.text = playerData.chosenCharacter.catName;
    }

    private void InitializePlayerName()
    {
        playerNameTMP.text = playerData.playerName;
    }

    void Start()
    {
        InitializeChosenCatName();
        InitializePlayerName();
    }
}