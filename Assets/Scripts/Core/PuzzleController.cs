using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuzzleController : MonoBehaviour
{
    [SerializeField] private PuzzleElement[] _puzzleElements;
    [SerializeField] private RectTransform[] _targets;
    [SerializeField] private Transform _contentScroll;
    [SerializeField] private Transform _content;

    [SerializeField] private ScrollRect _scrollRect;
    private readonly List<GameObject> PuzzleList = new();



    private Sprite _sprite;

    [SerializeField] private UnityEngine.UI.Image _image;
    public Sprite Sprite 
    { 
        private get => _sprite;
        set 
        {
            _sprite = value;
            Game.Action.SendStartGame();
            _image.sprite = value;
        } 
    }

    public Transform Content => _content;

    private void Start() 
    {
        Game.Action.OnStartGame.AddListener(StartGame);

        for (int i = 0; i < _puzzleElements.Length; i++)
            PuzzleList.Add(_puzzleElements[i].gameObject);        
    } 

    private void StartGame()
    {
        for(int i = 0; i < _puzzleElements.Length; i++)
        {
            _puzzleElements[i].Sprite = Sprite;
            _puzzleElements[i].transform.SetParent(_contentScroll);
            _puzzleElements[i].IsComplated = false;
        }

        Shuffle();
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

    public bool Init;
    private void OnValidate()
    {
        if(Init)
        {
            for (int i = 0; i < _targets.Length; i++)           
                _puzzleElements[i].Target = _targets[i];
            
            Init = false;
        }
    }
}