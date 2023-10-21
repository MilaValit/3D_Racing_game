using UnityEngine;
using UnityEngine.UI;

public class UIHintStart : MonoBehaviour, IDependancy<RaceStateTracker>
{
    [SerializeField] private GameObject hint;
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;
    // Start is called before the first frame update
    void Start()
    {
        raceStateTracker.PreparationStarted += OnPreparationStarted;

        hint.SetActive(true);
    }

    private void OnDestroy()
    {
        raceStateTracker.PreparationStarted -= OnPreparationStarted;        
    }
    
    private void OnPreparationStarted()
    {
        hint.SetActive(false);
    }
}
