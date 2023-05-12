using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Fruit : MonoBehaviour
{
    [SerializeField] private int _reward;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Player>(out Player player))
        {
            player.IncreaseScore(_reward);
            gameObject.SetActive(false);
        }
    }
}