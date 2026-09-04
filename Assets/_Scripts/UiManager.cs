using System;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    public static event Action OnBetting;
    
    [SerializeField] private Animator _buttonAnim;

    public void Betting()
    {
        OnBetting?.Invoke();
        _buttonAnim.SetTrigger("ButtonPressed");
    }

}
