using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(ButtonBase))]
public abstract class BaseSpriteSwapView : MonoBehaviour
{
    private Image _image;
    private ButtonBase _button;
    protected AudioSpriteSwap _audioSpriteSwap;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _button = GetComponent<ButtonBase>();
    }

    void Start()
    {
        _audioSpriteSwap = Game.Locator.AudioSettings as AudioSpriteSwap;
        _button.OnClick.AddListener(Swap);
        UpdateState();
    }

    private void OnEnable()
    {
        if (_audioSpriteSwap == null) return;
        UpdateState();
    }

    protected void UpdateState()
    {
        _image.sprite = Get() ? On() : Off();
    }

    protected abstract Sprite On();
    protected abstract Sprite Off();

    protected abstract bool Get();
    protected abstract void Swap();
}