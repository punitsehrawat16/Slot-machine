using UnityEngine;

public class SlotOutcomeGenerator : MonoBehaviour
{
    public int GetRandomSymbolId(int _maxSymbols) => Random.Range(0, _maxSymbols);
}