using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    //cell size is 1x1
    [SerializeField] private GemsCollection _gemsCollection;
    [SerializeField] private Vector2Int _gridSize;

    private Gem[,] _gems;


    private void Start()
    {
        Generate();
    }

    void Generate()
    {
        _gems = new Gem[_gridSize.y, _gridSize.x];
        
        Vector2 position = new Vector2( (_gridSize.x-1)/-2f,  (_gridSize.y-1)/-2f);
        
        for (int i = 0; i < _gridSize.y; i++)
        {
            for (int j = 0; j < _gridSize.x; j++)
            {
                _gems[i, j] = Instantiate(_gemsCollection.RandomItem);
                _gems[i, j].transform.position = position;
                position.x++;
            }
            position.y++;
            position.x = (_gridSize.x-1)/-2f;
        }
    }
    
    
}
