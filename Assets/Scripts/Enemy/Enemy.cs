using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private int _speed;
    [SerializeField] private TargetPoint _targetPoint;
    [SerializeField] private int _patrolAreaLength;
    [SerializeField] private bool _moveRightFirst;
    [SerializeField] private int _damage;

    private SpriteRenderer _spriteRenderer;
    private int _directionIndicator;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _targetPoint = Instantiate(_targetPoint, new Vector2(0, 0), Quaternion.identity);

        if (_moveRightFirst)
        {
            _spriteRenderer.flipX = true;
            _directionIndicator = 1;
        }
        else
        {
            _directionIndicator = -1;
        }

        SetNextTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.transform.position, _speed * Time.deltaTime);
    }

    public void SetNextTarget()
    {
        float deltaX = transform.position.x + _patrolAreaLength * _directionIndicator;
        Vector2 nextPosition = new Vector2(deltaX, transform.position.y);
        _targetPoint.transform.position = nextPosition;

        _directionIndicator *= -1;
        _spriteRenderer.flipX = !_spriteRenderer.flipX;
    }
}