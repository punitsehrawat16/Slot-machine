using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
public class MachineBrain : MonoBehaviour
{
    [SerializeField] private SlotOutcomeGenerator _outcomeGenerator;
    [SerializeField] private WinManager _winManager;
    [Header("Reels Data")]
    [SerializeField] private List<ReelSpin> _reels;
    [SerializeField] private int _maxSymbols;
    [SerializeField] private float _reelAnimationSpeed;
    
    [Header("Result Timers")]
    [SerializeField] private float _reelsResultDifference;
    [SerializeField] private float _waitTimeResult;
    
    //other
    Coroutine _betting;
    private int reel1;
    private int reel2;
    private int reel3;
    private void OnEnable()
    {
        UiManager.OnBetting += Bet;
    }

    private void Start()
    {
        foreach (var reel in _reels)
        {
            reel.SetSpeed(_reelAnimationSpeed);
        }
    }

    void OnDisable()
    {
        UiManager.OnBetting -= Bet;
        
        if(_betting!=null)
            StopCoroutine(_betting);
    }
    
    public void Bet()
    {
        Debug.Log("Betting started");
        if(_betting !=null)
            StopCoroutine(_betting);
        
        _betting = StartCoroutine(StartMachine());
    }
    
    IEnumerator StartMachine()
    {
        foreach (var reel in _reels)
        {
            reel.StartSpin();
        }
        
        yield return new WaitForSeconds(_waitTimeResult);
        reel1 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[0].SetResult(reel1);
        yield return new WaitForSeconds(_reelsResultDifference);
        reel2 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[1].SetResult(reel2);
        yield return new WaitForSeconds(_reelsResultDifference);
        reel3 = _outcomeGenerator.GetRandomSymbolId(_maxSymbols);
        _reels[2].SetResult(reel3);
        
        _winManager.CheckResult(reel1,reel2,reel3);
    }
}
