using UnityEngine;

public class TargetPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Enemy>(out Enemy enemy))
            enemy.SetNextTarget();
    }
}