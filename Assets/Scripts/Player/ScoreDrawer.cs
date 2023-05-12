using TMPro;
using UnityEngine;

public class ScoreDrawer : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _score;

    public void ChangeScoreText()
    {
        _score.text = _player.Score.ToString();
    }
}
