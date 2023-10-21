using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrackTime : MonoBehaviour, IDependancy<RaceTimeTracker>, IDependancy<RaceStateTracker>
{
    [SerializeField] private Text text;

    private RaceTimeTracker timeTracker;
    public void Construct(RaceTimeTracker obj) => timeTracker = obj;

    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Start()
    {
        raceStateTracker.Started += OnRaceStarted;
        raceStateTracker.Completed += OnRaceCompleted;

        text.enabled = false;
    }

    private void OnDestroy()
    {
        raceStateTracker.Started -= OnRaceStarted;
        raceStateTracker.Completed -= OnRaceCompleted;
    }
    private void OnRaceStarted()
    {
        text.enabled = true;
        enabled = true;
    }

    private void OnRaceCompleted()
    {
        text.enabled = false;
        enabled = false;
    }

    private void Update()
    { 
        text.text = StringTime.SecondToTimeString(timeTracker.CurrentTime);

    }
}
