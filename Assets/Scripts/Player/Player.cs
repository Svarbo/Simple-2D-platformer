using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;
    [SerializeField] private UnityEvent _playerDied;
    [SerializeField] private UnityEvent _playerAppeared;
    [SerializeField] private UnityEvent _fruitRaised;

    private int _currentHealth;
    private int _score;

    public int Score => _score;
    public int CurrentHealth => _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
        _playerAppeared?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;

        if( _currentHealth <= 0)
            Die();
    }

    public void IncreaseScore(int reward)
    {
        _score += reward;
        _fruitRaised?.Invoke();
    }

    private void Die()
    {
        Time.timeScale = 0;

        _playerDied?.Invoke();
    }
}