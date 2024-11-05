using UnityEngine;

public class WikiElement : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private bool _isDefault;

    [Space(10)]
    [SerializeField] private GameObject _enable;
    [SerializeField] private GameObject _disable;

    [Space(10)]
    [SerializeField] private ButtonBase _button;

    [Space(10)]
    [SerializeField] private string _title;
    [SerializeField] private string _description;

    private readonly string SaveName = "Stage";

    private void Start() => _button.OnClick.AddListener(OpenPanel);

    private void OnEnable()
    {
        bool isActive = _isDefault || PlayerPrefs.GetInt(SaveName + _id, 0) == 1;
        _enable.SetActive(isActive);
        _disable.SetActive(!isActive);
    }

    private void OpenPanel() => WikiPanel.Instance.OpenPanel(_title, _description);
}