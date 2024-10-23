using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PuzzleElement[] _puzzleElements;
    [SerializeField] private GridLayoutGroup _grid;

    [Space(10)]
    [SerializeField] private Transform _contentScroll;
    [SerializeField] private Transform _content;
    [SerializeField] private Transform _contentComplated;

    [Space(10)]
    [SerializeField] private RectTransform _shadow;
    [SerializeField] private ScrollRect _scrollRect;
    [SerializeField] private Image _image;

    private readonly List<GameObject> PuzzleList = new();
    private readonly Vector2 Pivot = new(0.5f, 0.5f);

    public RectTransform Shadow => _shadow;
    public Transform Content => _content;
    public Transform ContentComplated => _contentComplated;

    private void Start() 
    {
        for (int i = 0; i < _puzzleElements.Length; i++)
            PuzzleList.Add(_puzzleElements[i].gameObject);        
    } 

    public void SetSetting(ButtonStage stage)
    {
        int hw = 1024 / stage.Count;
        int a = 0;

        Vector2 size = new(hw, hw);

        _grid.cellSize = size;
        _shadow.sizeDelta = size * 1.1f;

        _image.sprite = Sprite.Create(stage.Texture, new Rect(0, 0, 1024, 1024), Pivot);

        for (int i = stage.Count - 1; i >= 0; i--)
        {
            for (int j = 0; j < stage.Count; j++)
            {
                Rect rect = new(j * hw, i * hw, hw, hw);
                _puzzleElements[a].Sprite = Sprite.Create(stage.Texture, rect, Pivot);
                _puzzleElements[a].gameObject.SetActive(true);
                _puzzleElements[a].transform.SetParent(_contentScroll);
                _puzzleElements[a].IsComplated = false;
                a++;
            }
        }

        for (; a < _puzzleElements.Length; a++)
        {
            _puzzleElements[a].gameObject.SetActive(false);
            _puzzleElements[a].IsComplated = true;
        }    
            
        Shuffle();
        Game.Action.SendStartGame();
    }

    private void Shuffle()
    {
        for (int i = 0; i < PuzzleList.Count; i++)
        {
            int randomIndex = Random.Range(i, PuzzleList.Count);
            GameObject temp = PuzzleList[i];
            PuzzleList[i] = PuzzleList[randomIndex];
            PuzzleList[randomIndex] = temp;
        }

        RepositionObjects();
    }

    private void RepositionObjects()
    {
        for (int i = 0; i < PuzzleList.Count; i++)
        {
            PuzzleList[i].transform.SetSiblingIndex(i);
        }

        LayoutRebuilder.MarkLayoutForRebuild(_scrollRect.content.GetComponent<RectTransform>());
    }

    public bool CheckComplated()
    {
        foreach(PuzzleElement puzzle in _puzzleElements)        
            if (!puzzle.IsComplated) return false;
        
        return true;
    }
}