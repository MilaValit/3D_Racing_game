using UnityEngine;
using UnityEngine.UI;

public class UICountDownTimer : MonoBehaviour, IDependancy<RaceStateTracker>
{
    [SerializeField] private Text text;
    [SerializeField] private Timer countDownTimer;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;
        raceStateTracker.Started += OnRaceStarted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;
        raceStateTracker.Started -= OnRaceStarted;
    }

    private void OnRaceStarted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void OnPreparationStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void Update()
    {
        text.text = raceStateTracker.CountDouwnTimer.Value.ToString("F0");

        if (text.text == "0")
            text.text = "GO!";
    }
}
