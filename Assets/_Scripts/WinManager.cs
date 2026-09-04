using UnityEngine;

public class WinManager : MonoBehaviour
{
    public void CheckResult(int reel1, int reel2, int reel3)
    {
        if (reel1 == reel2 && reel2 == reel3)
        {
            Debug.Log("WIN!");
        }
        else
        {
            Debug.Log("LOSE!");
        }
    }
}