using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineBrain : MonoBehaviour
{
    // UI listens to this event to display betting, win, and loss messages.
    public static event Action<string> OnDisplayMessage;
    public static event Action<bool> OnMAchineInteraction;

    [Header("References")]
    [SerializeField] private SlotOutcomeGenerator _outcomeGenerator;
    [SerializeField] private WinManager _winManager;
    [SerializeField] private PayManager _payManager;

    [Header("Reels")]
    [SerializeField] private List<ReelSpin> _reels;
    [SerializeField] private int _maxSymbols;
    [SerializeField] private float _reelAnimationSpeed;

    [Header("Result Timers")]
    [SerializeField] private float _reelResultDifference;
    [SerializeField] private float _resultWaitTime;

    private Coroutine _bettingCoroutine;

    private int _reel1;
    private int _reel2;
    private int _reel3;

    private void OnEnable()
    {
        UiManager.OnBetting += Bet;
    }

    private void Start()
    {
        foreach (ReelSpin reel in _reels)
        {
            reel.SetSpeed(_reelAnimationSpeed);
        }
    }

    private void OnDisable()
    {
        UiManager.OnBetting -= Bet;

        if (_bettingCoroutine != null)
        {
            StopCoroutine(_bettingCoroutine);
        }
    }

    public void Bet(int betAmount)
    {
        _payManager.SetBetAmount(betAmount);
        OnDisplayMessage?.Invoke("-" + betAmount + " Debited");

        // Prevent multiple spin sequences from running at the same time.
        if (_bettingCoroutine != null)
        {
            StopCoroutine(_bettingCoroutine);
        }

        _bettingCoroutine = StartCoroutine(StartMachine());
    }

    private IEnumerator StartMachine()
    {
        OnMAchineInteraction?.Invoke(false);
        
        // Start all reels together and stop them sequentially.
        foreach (ReelSpin reel in _reels)
        {
            reel.StartSpin();
        }
        
        yield return new WaitForSeconds(_resultWaitTime);

        _reel1 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[0].SetResult(_reel1);

        yield return new WaitForSeconds(_reelResultDifference);

        _reel2 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[1].SetResult(_reel2);

        yield return new WaitForSeconds(_reelResultDifference);

        _reel3 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[2].SetResult(_reel3);

        // A payout is awarded only when all three reels contain the same symbol.
        if (_winManager.IsWinningCombination(_reel1, _reel2, _reel3))
        {
            int payout = _payManager.CalculatePayout(_reel1);
            OnDisplayMessage?.Invoke("You Won! +" + payout + " Credits");
        }
        else
        {
            OnDisplayMessage?.Invoke("You Lost!");
        }
        OnMAchineInteraction?.Invoke(true);

    }
}