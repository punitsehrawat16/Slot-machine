using System;
using UnityEngine;

public class PayManager : MonoBehaviour
{
    public static event Action<int> OnDisplayWallet; 
    [SerializeField] private int _wallet = 1000;

    private int _betAmount;

    void Start()
    {
        OnDisplayWallet?.Invoke(_wallet);
    }
    public void SetBetAmount(int betAmount)
    {
        _betAmount = betAmount;
        _wallet -= betAmount;
        OnDisplayWallet?.Invoke(_wallet);
    }

    public int CalculatePayout(int symbolId)
    {
        int multiplier = GetMultiplier(symbolId);
        int payout = _betAmount * multiplier;

        AddToWallet(payout);

        return payout;
    }

    private int GetMultiplier(int symbolId)
    {
        switch (symbolId)
        {
            case 0: return 10; // Seven
            case 1: return 2;  // Cherry
            case 2: return 3;  // Bell
            case 3: return 5;  // BAR
            default: return 0;
        }
    }

    private void AddToWallet(int payout)
    {
        _wallet += payout;
        OnDisplayWallet?.Invoke(_wallet);
    }
}