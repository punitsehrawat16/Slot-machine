using System;
using System.Collections.Generic;
using UnityEngine;

public class ReelSpin : MonoBehaviour
{
    [SerializeField] private List<GameObject> Symbols;
    public const float yUpperBound = 3f;
    private const float yLowerBound = 0f;
    [SerializeField] private float _moveSpeed;

    public bool isSpin;
    private void Update()
    { 
        if(isSpin)
            Spin();
    }

    void Spin()
    {
        foreach (var g in Symbols )
        {
            var pos = g.transform.position;
            pos.y -= _moveSpeed * Time.deltaTime;
            g.transform.position = pos;
            Debug.Log(pos.y+"  :Y-----------------POS:  "+pos);
            if (g.transform.position.y < yLowerBound)
            {
                Debug.Log("Reset");
                pos.y = yUpperBound;
                g.transform.position = pos;
            }
        }
    }
}
