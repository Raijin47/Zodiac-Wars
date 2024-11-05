using TMPro;
using UnityEngine;


public class WikiPanel : MonoBehaviour
{
    public static WikiPanel Instance;

    [SerializeField] private GameObject _panel;

    [SerializeField] private TextMeshProUGUI _textTitle;
    [SerializeField] private TextMeshProUGUI _textDescription;

    private void Awake()
    {
        Instance = this;
    }

    public void OpenPanel(string title, string description)
    {
        _panel.SetActive(true);
        _textTitle.text = title;
        _textDescription.text = description;
    }
}