using System.Collections;
using UnityEngine;

public class RouletteSpin : MonoBehaviour
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ButtonBase _spinButton;

    [SerializeField] private int[] _rewards;

    [Space(10)]
    [SerializeField] private float MinRotatePower = 1000;
    [SerializeField] private float MaxRotatePower = 2500;
    [SerializeField] private float StopPower = 400;

    private Coroutine _rouletteSpinCoroutine;

    private readonly WaitForSeconds Delay = new(1f);

    private void Start()
    {
        _spinButton.OnClick.AddListener(StartSpin);
    }

    private void StartSpin()
    {
        _spinButton.Interactable = false;

        Game.Audio.PlayClip(2);

        _rigidbody.AddTorque(Random.Range(MinRotatePower, MaxRotatePower));

        if (_rouletteSpinCoroutine != null)
        {
            StopCoroutine(_rouletteSpinCoroutine);
            _rouletteSpinCoroutine = null;
        }
        _rouletteSpinCoroutine = StartCoroutine(RouletteSpinProcess());
    }

    private IEnumerator RouletteSpinProcess()
    {
        yield return Delay;

        while(_rigidbody.angularVelocity > 0)
        {
            _rigidbody.angularVelocity -= StopPower * Time.deltaTime;
            _rigidbody.angularVelocity = Mathf.Clamp(_rigidbody.angularVelocity, 0, MaxRotatePower);
            yield return null;
        }

        Result();

        _spinButton.Interactable = true;
    }

    private void Result()
    {        
        int whatWeWin = Mathf.RoundToInt(_transform.eulerAngles.z / 360 / _rewards.Length);
        if (whatWeWin == _rewards.Length) whatWeWin = 0;
        Game.Wallet.Add(_rewards[whatWeWin]);
    }
}