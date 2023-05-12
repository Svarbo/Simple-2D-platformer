using TMPro;
using UnityEngine;

public class ReplicaManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _replicaText;

    public void ShowFirstReplica()
    {
        _replicaText.gameObject.SetActive(true);
    }

    public void ShowFinalReplica()
    {
        _replicaText.text = "GG WP";
    }
}
