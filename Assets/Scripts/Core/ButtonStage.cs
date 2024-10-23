using TMPro;
using UnityEngine;

public class ButtonStage : MonoBehaviour
{
    [SerializeField] private int _id;

    [Space(10)]
    [SerializeField] private Texture2D _texture;
    [SerializeField] private int _size = 8;

    [Space(10)]
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private int _price;

    [Space(10)]
    [SerializeField] private GameObject _enable;
    [SerializeField] private GameObject _disable;

    private readonly string SaveName = "Stage";

    public int Count => _size;
    public Texture2D Texture => _texture;
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
        if(_priceText != null) _priceText.text = _price.ToString();
    }
}