using TMPro;
using UnityEngine;

public class ButtonStage : MonoBehaviour
{
    [SerializeField] private int _id;
    [SerializeField] private int _price;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private GameObject _enable, _disable;

    private readonly string SaveName = "Stage";

    public int Price => _price;
    public bool IsPurchased
    {
        get => PlayerPrefs.GetInt(SaveName + _id, 0) == 1;
        set
        {
            PlayerPrefs.SetInt(SaveName + _id, value ? 1 : 0);
            _enable.SetActive(value);
            _disable.SetActive(!value);
        }
    }

    private void Start()
    {
        IsPurchased = PlayerPrefs.GetInt(SaveName + _id, 0) == 1;
        _priceText.text = _price.ToString();
    }
}