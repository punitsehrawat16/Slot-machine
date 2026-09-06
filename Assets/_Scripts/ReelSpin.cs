using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SymbolData
{
    public Transform transform;
    public int id;
}

public class ReelSpin : MonoBehaviour
{
    [SerializeField] private List<SymbolData> _symbols = new List<SymbolData>();

    [SerializeField] private float _targetYPos = 2f;
    [SerializeField] private float _symbolDifference = 1f;

    [SerializeField] private float _yLowerBound = -.5f;

    private float _moveSpeed;
    private int _resultId;

    private bool _isSpinning;
    private bool _hasResult;

    private void Update()
    {
        if (_isSpinning)
        {
            Spin();
        }

        if (_hasResult)
        {
            StopAtTarget();
        }
    }

    public void SetSpeed(float speed)
    {
        _moveSpeed = speed;
    }

    public void StartSpin()
    {
        _isSpinning = true;
        _hasResult = false;
    }

    public void SetResult(int symbolId)
    {
        _resultId = symbolId;
        _hasResult = true;
    }

    private void Spin()
    {
        foreach (SymbolData symbol in _symbols)
        {
            Vector3 position = symbol.transform.position;

            position.y -= _moveSpeed * Time.deltaTime;

            // Move the symbol back to the top when it passes the lower boundary,
            // creating a continuous looping reel animation.
            if (position.y < _yLowerBound)
            {
                position.y = GetHighestPos() + _symbolDifference;
            }

            symbol.transform.position = position;
        }
    }

    private void StopAtTarget()
    {
        int index = _resultId;

        Transform target = _symbols[index].transform;

        if (target.position.y > _targetYPos)
            return;

        // Move the entire reel by the same distance so the target symbol
        // aligns with the designated stopping position.
        float difference = _targetYPos - target.position.y;

        foreach (SymbolData symbol in _symbols)
        {
            Vector3 position = symbol.transform.position;
            position.y += difference;
            symbol.transform.position = position;
        }

        _isSpinning = false;
        _hasResult = false;
    }

    private float GetHighestPos()
    {
        float highestY = float.MinValue;

        foreach (SymbolData symbol in _symbols)
        {
            if (symbol.transform.position.y > highestY)
            {
                highestY = symbol.transform.position.y;
            }
        }

        return highestY;
    }
}