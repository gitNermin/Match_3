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
                var gem = _gemsCollection.RandomItem;
                while (HasSimilarGems(gem, i, j))
                {
                    gem = _gemsCollection.RandomItem;
                }
                _gems[i, j] = Instantiate(gem);
                _gems[i, j].transform.position = position;
                position.x++;
            }
            position.y++;
            position.x = (_gridSize.x-1)/-2f;
        }
    }

    bool HasSimilarGems(Gem gem, int row, int col)
    {
        if (col >= 2 && _gems[row, col-1].Type == gem.Type && _gems[row, col-1].Type == gem.Type)
        {
            return true;
        }

        if (row >= 2 && _gems[row-1, col].Type == gem.Type && _gems[row-2, col].Type == gem.Type)
        {
            return true;
        }

        return false;
    }

    public void Regenerate()
    {
        for (int i = 0; i < _gridSize.y; i++)
        {
            for (int j = 0; j < _gridSize.x; j++)
            {
                Destroy(_gems[i, j].gameObject);
            }
        }
        
        Generate();
    }
    
    
}
