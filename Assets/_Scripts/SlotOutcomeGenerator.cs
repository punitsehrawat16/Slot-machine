using UnityEngine;

public class SlotOutcomeGenerator : MonoBehaviour
{
    public int GetRandomSymbolId(int _maxSymbols)
    {
        int random = Random.Range(0, _maxSymbols);
        Debug.Log(random);
        return random;
    }
}