using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _image;
    [SerializeField] private RectTransform _main;
    [SerializeField] private RectTransform _target;

    private const float _offset = 50f;
    private bool _isComplated;

    public bool IsComplated 
    { 
        get => _isComplated; 
        set
        {
            _isComplated = value;
            _image.materialForRendering.SetFloat("_UVDistortFade", value ? 1 : 0);
        }
    }

    public Sprite Sprite 
    { 
        set 
        {
            _image.sprite = value;
            _image.SetNativeSize();
        }
    }

    void Start() => _image.material = Instantiate(_image.material);

    public void OnDrag(PointerEventData eventData)
    {
        if (IsComplated) return;
        transform.position = Input.mousePosition;
        Game.Puzzle.Shadow.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsComplated) return;

        Game.Puzzle.Shadow.transform.position = transform.position;
        Game.Puzzle.Shadow.gameObject.SetActive(true);

        transform.localScale = Vector3.one * 1.1f;

        transform.SetParent(Game.Puzzle.Content);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsComplated) return;
        Game.Audio.PlayClip(0);
        Game.Puzzle.Shadow.gameObject.SetActive(false);
        transform.localScale = Vector3.one;

        if (Mathf.Abs(_main.transform.position.x - _target.transform.position.x) <= _offset &&
            Mathf.Abs(_main.transform.position.y - _target.transform.position.y) <= _offset)
        {
            _main.anchoredPosition = _target.anchoredPosition;
            transform.SetParent(Game.Puzzle.ContentComplated);
            IsComplated = true;

            if (Game.Puzzle.CheckComplated())
            {
                Game.Action.SendWin();
                Game.Audio.PlayClip(3);
            }
        }
    }
}