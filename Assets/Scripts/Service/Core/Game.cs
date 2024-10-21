using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static Game Locator;
    [SerializeField] private PuzzleController _puzzle;

    [Space(10)]
    [SerializeField] private GameAudio _audio;
    [SerializeField] private GameAction _gameAction;

    [Space(10)]
    [SerializeReference, SubclassSelector] private Wallet _walletType;
    [SerializeReference, SubclassSelector] private AudioSettings _audioType;

    [Space(10)]
    [SerializeReference, SubclassSelector] private List<Component> _components;

    public static GameAudio Audio;
    public static Wallet Wallet;
    public static GameAction Action;

    public static PuzzleController Puzzle;
    public AudioSettings AudioSettings => _audioType;

    private void Awake()
    {
        Locator = this;
        Audio = _audio;
        Wallet = _walletType;
        Action = _gameAction;

        Puzzle = _puzzle;

        foreach (Component component in _components)
            component.Init();
    }

    private void Start()
    {
        _walletType.Init();
        //_audioType.Init();
        //_audio.Init();
    }

    public T Get<T>() where T : Component
    {
        return (T)_components.FirstOrDefault(component => component is T);
    }

    public void StartGame() => Action.SendStartGame();
    public void GameOver() => Action.SendGameOver();
    public void AddMoney(int value) => Wallet.Add(value); 
}