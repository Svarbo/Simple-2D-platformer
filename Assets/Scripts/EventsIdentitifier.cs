using UnityEngine;
using UnityEngine.Events;

public class EventsIdentitifier : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private FruitSpawner _fruitSpawner;
    [SerializeField] private UnityEvent _firstFruitRaised;
    [SerializeField] private UnityEvent _winConditionRaised;

    public void IdentifySpecialScoreValues()
    {
        if (_player.Score == 1)
            _firstFruitRaised?.Invoke();
        else if (_player.Score == _fruitSpawner.LevelNumberFruits)
            _winConditionRaised?.Invoke();
    }
}
