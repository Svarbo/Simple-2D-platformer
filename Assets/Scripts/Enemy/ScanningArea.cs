using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class ScanningArea : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Player>(out Player player))
        {
            _spriteRenderer.color = Color.red;
            player.TakeDamage(player.CurrentHealth);
        }
    }
}
