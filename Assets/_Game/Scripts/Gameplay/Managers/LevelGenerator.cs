using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    private const int MinSolvablePossibilities = 3;

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

        Vector2 position = new Vector2((_gridSize.x - 1) / -2f, (_gridSize.y - 1) / -2f);

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
            position.x = (_gridSize.x - 1) / -2f;
        }

        if (!HasEnoughSolutions()) Regenerate();
    }

    bool HasSimilarGems(Gem gem, int row, int col)
    {
        if (col >= 2 && _gems[row, col - 1].Type == gem.Type && _gems[row, col - 1].Type == gem.Type)
        {
            return true;
        }

        if (row >= 2 && _gems[row - 1, col].Type == gem.Type && _gems[row - 2, col].Type == gem.Type)
        {
            return true;
        }

        return false;
    }

    bool HasEnoughSolutions()
    {
        int solutions = 0;
        bool[,] takenTiles = new bool[_gridSize.y, _gridSize.x];
        for (int i = 0; i < _gridSize.y; i++)
        {
            for (int j = 0; j < _gridSize.x; j++)
            {
                if (takenTiles[i, j]) continue;

                var type = _gems[i, j].Type;
                if (j < _gridSize.x - 2)
                {
                    if (i < _gridSize.y - 1)
                    {
                        if (!takenTiles[i, j + 1] && !takenTiles[i + 1, j + 2] && _gems[i, j + 1].Type == type &&
                            _gems[i + 1, j + 2].Type == type)
                        {
                            takenTiles[i, j + 1] = true;
                            takenTiles[i + 1, j + 2] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }

                        if (!takenTiles[i + 1, j + 1] && !takenTiles[i, j + 2] && _gems[i + 1, j + 1].Type == type &&
                            _gems[i, j + 2].Type == type)
                        {
                            takenTiles[i + 1, j + 1] = true;
                            takenTiles[i, j + 2] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }
                    }

                    if (i > 0)
                    {
                        if (!takenTiles[i, j + 1] && !takenTiles[i - 1, j + 2] && _gems[i, j + 1].Type == type &&
                            _gems[i - 1, j + 2].Type == type)
                        {
                            takenTiles[i, j + 1] = true;
                            takenTiles[i - 1, j + 2] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }

                        if (!takenTiles[i - 1, j + 1] && !takenTiles[i, j + 2] &&
                            _gems[i - 1, j + 1].Type == type && _gems[i, j + 2].Type == type)
                        {
                            takenTiles[i - 1, j + 1] = true;
                            takenTiles[i, j + 2] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                        }
                    }
                }

                if (i < _gridSize.y - 2)
                {
                    if (j < _gridSize.x - 1)
                    {
                        if (!takenTiles[i + 1, j] && !takenTiles[i + 2, j + 1] && _gems[i + 1, j].Type == type &&
                            _gems[i + 2, j + 1].Type == type)
                        {
                            takenTiles[i + 1, j] = true;
                            takenTiles[i + 2, j + 1] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }

                        if (!takenTiles[i + 1, j + 1] && !takenTiles[i + 2, j] && _gems[i + 1, j + 1].Type == type &&
                            _gems[i + 2, j].Type == type)
                        {
                            takenTiles[i + 1, j + 1] = true;
                            takenTiles[i + 2, j] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }
                    }

                    if (j > 0)
                    {
                        if (!takenTiles[i + 1, j] && !takenTiles[i + 2, j - 1] && _gems[i + 1, j].Type == type &&
                            _gems[i + 2, j - 1].Type == type)
                        {
                            takenTiles[i + 1, j] = true;
                            takenTiles[i + 2, j - 1] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                            continue;
                        }

                        if (!takenTiles[i + 1, j - 1] && !takenTiles[i + 2, j] &&
                            _gems[i + 1, j - 1].Type == type && _gems[i + 2, j].Type == type)
                        {
                            takenTiles[i + 1, j - 1] = true;
                            takenTiles[i + 2, j] = true;
                            takenTiles[i, j] = true;

                            solutions++;
                            if (solutions >= MinSolvablePossibilities) return true;
                        }
                    }
                }
            }
        }

        Debug.Log("regenerate");
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