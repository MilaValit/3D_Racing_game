using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;


public enum RaceState
{
    Preparation,
    CountDown,
    Race,
    Passed
}

public class RaceStateTracker : MonoBehaviour, IDependancy<TrackpointCircuit>
{
    public event UnityAction PreparationStarted;
    public event UnityAction Started;
    public event UnityAction Completed;
    public event UnityAction<TrackPoint> TrackPointPassed;
    public event UnityAction<int> LapCompleted;


    private TrackpointCircuit trackPointCircuit;
    public void Construct(TrackpointCircuit trackpointCircuit) => this.trackPointCircuit = trackpointCircuit;

    [SerializeField] private Timer countdownTimer;
    [SerializeField] private int lapsToComplete;

    public Timer CountDouwnTimer => countdownTimer;

    private RaceState state;
    public RaceState State => state;

    private void StartState(RaceState state)
    {
        this.state = state;
    }

    private void Start()
    {
        StartState(RaceState.Preparation);

        countdownTimer.enabled = false;

        countdownTimer.Finished += OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTriggered += OnTrackPointTriggered;
        trackPointCircuit.LapCompleted += OnLapCompleted;
    }

    private void OnDestroy()
    {
        countdownTimer.Finished -= OnCountdownTimerFinished;

        trackPointCircuit.TrackPointTriggered -= OnTrackPointTriggered;
        trackPointCircuit.LapCompleted -= OnLapCompleted;
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        TrackPointPassed?.Invoke(trackPoint);
    }
    private void OnCountdownTimerFinished()
    {
        StartRace();
    }

    private void OnLapCompleted(int lapAmount)
    {
        if (trackPointCircuit.Type == TrackType.Sprint)
        {
            CompleteRace();
        }

        if (trackPointCircuit.Type == TrackType.Circular)
        {
            if (lapAmount == lapsToComplete)
                CompleteRace();
            else
                CompleteLap(lapAmount);
        }
    }

    public void LaunchPreparationStart()
    {
        if (state != RaceState.Preparation) return;
        StartState(RaceState.CountDown);

        countdownTimer.enabled = true;


        PreparationStarted?.Invoke();
    }

    private void StartRace()
    {
        if (state != RaceState.CountDown) return;
        StartState(RaceState.Race);

        Started?.Invoke();
    }

    private void CompleteRace()
    {
        if (state != RaceState.Race) return;
        StartState(RaceState.Passed);

        Completed?.Invoke();

        UnlockNewLevel();
    }

    private void CompleteLap(int lapAmount)
    {
        LapCompleted?.Invoke(lapAmount);
    }

    private void UnlockNewLevel()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReacherIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }

}
