using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PuzzleElement : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    [SerializeField] private Image _puzzleImg;
    [SerializeField] private RectTransform _main;
    [SerializeField] private RectTransform _target;

    private const float _offset = 50f;
    public bool IsComplated { get; set; }

    public RectTransform Target { get => _target; set => _target = value; }
    public Sprite Sprite { set => _puzzleImg.sprite = value; }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsComplated) return;
        transform.position = Input.mousePosition;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsComplated) return;
        transform.localScale = Vector3.one * 1.1f;
        transform.SetParent(Game.Puzzle.Content);
        transform.SetAsLastSibling();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsComplated) return;

        transform.localScale = Vector3.one;

        if (Mathf.Abs(_main.transform.position.x - _target.transform.position.x) <= _offset &&
            Mathf.Abs(_main.transform.position.y - _target.transform.position.y) <= _offset)
        {
            _main.anchoredPosition = _target.anchoredPosition;
            IsComplated = true;
            if(Game.Puzzle.CheckComplated())           
                Game.Action.SendWin();           
        }
    }

    private void OnValidate()
    {
        _main ??= GetComponent<RectTransform>();
        _puzzleImg ??= transform.GetChild(0).GetComponent<Image>();
    }
}