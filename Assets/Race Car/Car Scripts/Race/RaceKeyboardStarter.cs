using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceKeyboardStarter : MonoBehaviour, IDependancy<RaceStateTracker>
{
    private RaceStateTracker raceStateTracker;
    public void Construct(RaceStateTracker obj) => raceStateTracker = obj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) == true)
        {
            raceStateTracker.LaunchPreparationStart();
        }
    }
}
