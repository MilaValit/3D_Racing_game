using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum TrackType
{
    Circular,
    Sprint
}


public class TrackpointCircuit: MonoBehaviour
{
    public event UnityAction<TrackPoint> TrackPointTriggered;
    public event UnityAction<int> LapCompleted;

    [SerializeField] private TrackType type;
    public TrackType Type => type;

    private TrackPoint[] points;

    private int LapsCompleted = -1;

    private void Awake()
    {
        BuildCircuit();
    }

    private void Start()
    {
        for (int i =0; i< points.Length; i++)
        {
            points[i].Triggered += OnTrackPointTriggered;
        }

        points[0].AssignAsTarget();
    }

    private void OnDestroy()
    {
        for (int i = 0; i < points.Length; i++)
        {
            points[i].Triggered -= OnTrackPointTriggered;
        }
    }

    private void OnTrackPointTriggered(TrackPoint trackPoint)
    {
        if (trackPoint.IsTarget == false) return;

        trackPoint.Passed();
        trackPoint.Next?.AssignAsTarget();

        TrackPointTriggered?.Invoke(trackPoint);

        if (trackPoint.IsLast == true)
        {
            LapsCompleted++;

            if (type == TrackType.Sprint)
                LapCompleted?.Invoke(LapsCompleted);

            if (type == TrackType.Circular)
                if (LapsCompleted > 0)
                    LapCompleted?.Invoke(LapsCompleted);

        }
    }

    [ContextMenu(nameof(BuildCircuit))]
    private void BuildCircuit()
    {
        points = TrackCircuitBuilder.Build(transform, type);
    }
}
