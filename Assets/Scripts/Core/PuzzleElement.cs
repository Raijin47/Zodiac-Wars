using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _puzzleImg;
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
            _puzzleImg.materialForRendering.SetFloat("_UVDistortFade", value ? 1 : 0);
        }
    }

    public Sprite Sprite { set => _puzzleImg.sprite = value; }

    void Start()
    {
        _puzzleImg.material = Instantiate(_puzzleImg.material);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsComplated) return;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsComplated) return;

        Game.Puzzle.Shadow.transform.SetParent(transform);
        Game.Puzzle.Shadow.gameObject.SetActive(true);
        Game.Puzzle.Shadow.anchoredPosition = Vector2.zero;
        Game.Puzzle.Shadow.SetAsFirstSibling();

        transform.localScale = Vector3.one * 1.1f;

        transform.SetParent(Game.Puzzle.Content);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsComplated) return;
        Game.Puzzle.Shadow.gameObject.SetActive(false);
        transform.localScale = Vector3.one;

        if (Mathf.Abs(_main.transform.position.x - _target.transform.position.x) <= _offset &&
            Mathf.Abs(_main.transform.position.y - _target.transform.position.y) <= _offset)
        {
            _main.anchoredPosition = _target.anchoredPosition;
            IsComplated = true;
            transform.SetAsFirstSibling();
            if (Game.Puzzle.CheckComplated())           
                Game.Action.SendWin();           
        }
    }
}