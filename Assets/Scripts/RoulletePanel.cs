using System;
using UnityEngine;

public class RoulletePanel : MonoBehaviour
{
    [SerializeField] private int _timeUntilReward;
    [SerializeField] private GameObject _roulettePanel;

    private string _lastRewardTimeStr;
    private readonly string SaveName = "DailySpin";

    public void OpenPanel()
    {
        if(GetSecondsUntilReward())
        {
            _roulettePanel.SetActive(true);
        }
    }


    private bool GetSecondsUntilReward()
    {
        _lastRewardTimeStr = PlayerPrefs.GetString(SaveName, string.Empty);

        if (DateTime.TryParse(_lastRewardTimeStr, out DateTime lastRewardTime))
        {
            DateTime currentTime = DateTime.UtcNow;
            TimeSpan timeSinceLastReward = currentTime - lastRewardTime;
            float secondsPassed = (float)timeSinceLastReward.TotalSeconds;
            float secondsUntilReward = _timeUntilReward - secondsPassed;

            return secondsUntilReward > 0 ? false : true;
        }

        return true;
    }

    public void TakeReward()
    {
        PlayerPrefs.SetString(SaveName, DateTime.UtcNow.ToString());
    }
}