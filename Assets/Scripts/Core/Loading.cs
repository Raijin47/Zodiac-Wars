using UnityEngine;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject _loadingPanel;
    [SerializeField] private GameObject _menuPage;
    public void End()
    {
        _loadingPanel.SetActive(false);
        _menuPage.SetActive(true);
    }
}