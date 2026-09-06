using UnityEngine;

public class WinManager : MonoBehaviour
{
    public bool IsWinningCombination(int reel1, int reel2, int reel3)
    {
        return reel1 == reel2 && reel2 == reel3;
    }
}