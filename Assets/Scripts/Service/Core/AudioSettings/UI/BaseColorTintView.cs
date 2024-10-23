using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonBase))]
public abstract class BaseColorTintView : MonoBehaviour
{
    private Image _image;
    private ButtonBase _button;
    protected AudioColorTint _audioColorTint;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<ButtonBase>();
    }

    void Start()
    {
        _audioColorTint = Game.Locator.AudioSettings as AudioColorTint;
        _button.OnClick.AddListener(Swap);
        UpdateState();
    }

    private void OnEnable()
    {
        if (_audioColorTint == null) return;
        UpdateState();
    }

    protected void UpdateState()
    {
        _image.sprite = Get() ? _audioColorTint.OnSprite : _audioColorTint.OffSprite;
    }

    protected abstract bool Get();
    protected abstract void Swap();
}