using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreView : MonoBehaviour
{
    [SerializeField] private string _prefix;
    private TextMeshProUGUI _text;
    private float _current;

    private const float Speed = 5;
    private Coroutine _changeProcess;
    private Score _score;

    private void Awake() => _text = GetComponent<TextMeshProUGUI>();

    private void OnEnable()
    {
        _score ??= Game.Locator.Get<Score>();

        _score.OnResetScore += ResetScore;
        _score.OnAddScore += Add;
        Set(_score.Current);
    }

    private void OnDisable()
    {
        _score.OnAddScore -= Add;
        _score.OnResetScore -= ResetScore;

        if (_changeProcess != null)
        {
            StopCoroutine(_changeProcess);
            _changeProcess = null;
        }
    }

    private void ResetScore() 
    {
        if (_changeProcess != null)
        {
            StopCoroutine(_changeProcess);
            _changeProcess = null;
        }
        Set(0);
    } 

    private void Set(float value)
    {
        _current = value;
        _text.text = $"{_prefix}{Mathf.RoundToInt(_current)}";
    }

    private void Add()
    {
        if (!gameObject.activeInHierarchy) return;

        if (_changeProcess != null)
        {
            StopCoroutine(_changeProcess);
            _changeProcess = null;
        }

        _changeProcess = StartCoroutine(AddScoreProcess());
    }

    private IEnumerator AddScoreProcess()
    {
        while (_current < _score.Current)
        {
            Set(Mathf.Lerp(_current, _score.Current, Time.deltaTime * Speed));
            yield return null;
        }

        Set(_score.Current);
    }
}