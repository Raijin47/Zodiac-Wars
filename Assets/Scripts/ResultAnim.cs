using UnityEngine;

public class ResultAnim : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _panel;

    public void Player()
    {
        _animator.Play("Play");
    }

    public void EndAnim()
    {
        _panel.SetActive(false);
    }
}