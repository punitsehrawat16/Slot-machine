using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using System.Collections.Generic;
public class MachineBrain : MonoBehaviour
{
    [Header("Reels Data")]
    [SerializeField] private List<ReelSpin> _reels;
    [SerializeField] private int _maxSymbols;
    [SerializeField] private float _reelAnimationSpeed;
    
    [Header("Result Timers")]
    [SerializeField] private float _reelsResultDifference;
    [SerializeField] private float _waitTimeResult;
    
    //other
    Coroutine _betting;

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
        _reels[0].SetResult(GetRandomId());
        yield return new WaitForSeconds(_reelsResultDifference);
        _reels[1].SetResult(GetRandomId());
        yield return new WaitForSeconds(_reelsResultDifference);
        _reels[2].SetResult(GetRandomId());
    }

    int GetRandomId() => Random.Range(0, _maxSymbols);
   /*int GetRandomId()
   {
       int x = Random.Range(0, _maxSymbols);
       Debug.Log(x);
       return x;
   }*/
}
