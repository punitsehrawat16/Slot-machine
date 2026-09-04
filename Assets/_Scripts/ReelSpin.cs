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
    [SerializeField] private List<SymbolData> symbols = new List<SymbolData>();

    [SerializeField] private float targetYPos = 2f;
    [SerializeField] private float symbolDifference = 1f;

    [SerializeField] private float yLowerBound = -0.5f;
    [SerializeField] private float yUpperBound = 3.5f;

    private float moveSpeed;
    private int resultId;

    private bool hasResult;
    private bool isSpinning;

    private void Update()
    {
        if (hasResult)
        {
            StopAtTarget();
            return;
        }

        if (isSpinning)
        {
            Spin();
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
    }

    public void StartSpin()
    {
        hasResult = false;
        isSpinning = true;
    }

    public void SetResult(int id)
    {
        resultId = id;
        hasResult = true;
    }

    private void Spin()
    {
        foreach (SymbolData symbol in symbols)
        {
            Vector3 position = symbol.transform.position;

            position.y -= moveSpeed * Time.deltaTime;

            if (position.y < yLowerBound)
            {
                position.y = GetHighestPos() + symbolDifference;
            }

            symbol.transform.position = position;
        }
    }

    private void StopAtTarget()
    {
        int index = GetIndex(resultId);

        if (index < 0)
        {
            isSpinning = false;
            hasResult = false;
            return;
        }

        Transform target = symbols[index].transform;

        if (target.position.y > targetYPos)
            return;

        float difference = targetYPos - target.position.y;

        foreach (SymbolData symbol in symbols)
        {
            Vector3 position = symbol.transform.position;
            position.y += difference;
            symbol.transform.position = position;
        }

        float reelLength = yUpperBound - yLowerBound;

        foreach (SymbolData symbol in symbols)
        {
            Vector3 position = symbol.transform.position;

            if (position.y < yLowerBound)
                position.y += reelLength;

            if (position.y > yUpperBound)
                position.y -= reelLength;

            symbol.transform.position = position;
        }

        isSpinning = false;
        hasResult = false;
    }

    private int GetIndex(int id)
    {
        for (int i = 0; i < symbols.Count; i++)
        {
            if (symbols[i].id == id)
                return i;
        }

        return -1;
    }

    private float GetHighestPos()
    {
        float highestY = float.MinValue;

        foreach (SymbolData symbol in symbols)
        {
            if (symbol.transform.position.y > highestY)
                highestY = symbol.transform.position.y;
        }

        return highestY;
    }
}