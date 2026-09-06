using System;
using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static event Action<int> OnBetting;

    [SerializeField] private List<Button> _betButtons;
    
    [SerializeField] private TextMeshProUGUI _infoText;
    [SerializeField] private TextMeshProUGUI _wallet;
    [SerializeField] private Animator _buttonAnim;

    private void OnEnable()
    {
        MachineBrain.OnDisplayMessage += SetInfoText;
        PayManager.OnDisplayWallet += SetWallet;
        MachineBrain.OnMAchineInteraction += SetMachineInteraction;
    }

    private void OnDisable()
    {
        MachineBrain.OnDisplayMessage -= SetInfoText;
        PayManager.OnDisplayWallet -= SetWallet;
        MachineBrain.OnMAchineInteraction -= SetMachineInteraction;
    }

    public void Betting(int betAmount)
    {
        OnBetting?.Invoke(betAmount);
        _buttonAnim.SetTrigger("ButtonPressed");
    }

    private void SetMachineInteraction(bool isInteractable)
    {
        foreach (var button in _betButtons)
        {
            button.interactable = isInteractable;
        }
    }
    private void SetInfoText(string text) => _infoText.text = text;
    private void SetWallet(int amount) => _wallet.text = amount.ToString();
}