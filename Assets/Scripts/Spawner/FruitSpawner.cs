using UnityEngine;

public class FruitSpawner : ObjectPool
{
    [SerializeField] private GameObject _fruitPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private int _levelNumberFruits;
    private int _lastSpawnPointIndex;

    public int LevelNumberFruits => _levelNumberFruits;

    private void Start()
    {
        _levelNumberFruits = _spawnPoints.Length;
        _lastSpawnPointIndex = 0;
        Initialize(_fruitPrefab);

        SetNextFruit();
    }

    public void SetNextFruit()
    {
        if(TryGetObject(out GameObject fruit) && _lastSpawnPointIndex != _levelNumberFruits)
        {
            fruit.SetActive(true);
            fruit.transform.position = _spawnPoints[_lastSpawnPointIndex].transform.position;
            _lastSpawnPointIndex++;
        }
    }
}