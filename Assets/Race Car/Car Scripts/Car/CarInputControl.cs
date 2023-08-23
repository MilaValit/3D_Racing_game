using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInputControl : MonoBehaviour
{
    [SerializeField] private Car car;
    [SerializeField] private AnimationCurve breakCurve;
    [SerializeField] private AnimationCurve steerCurve;

    [SerializeField] [Range(0.0f, 1.0f)] private float autoBreakStrength = 0.5f;

    private float wheelSpeed;
    private float verticalAxis;
    private float horizontalAxis;
    private float handbreakAxis;

    private void Update()
    {

        wheelSpeed = car.WheelSpeed;

        UpdateAxis();

        UpdateThrottleAndBrak();
        UpdateSteer();

        UpdateAutoBreak();
    }    

    private void UpdateThrottleAndBrak()
    {
        if (Mathf.Sign(verticalAxis) == Mathf.Sign(wheelSpeed) || Mathf.Abs(wheelSpeed) < 0.5f)
        {
            car.ThrottleControl = Mathf.Abs(verticalAxis);
            car.BrakeControl = 0;
        }
        else
        {
            car.ThrottleControl = 0;
            car.BrakeControl = breakCurve.Evaluate(wheelSpeed / car.MaxSpeed);
        }

        //gears
        if (verticalAxis < 0 && wheelSpeed > -0.5f && wheelSpeed <= 0.5f)
        {
            car.ShiftToReversGear();
        }
        if (verticalAxis > 0 && wheelSpeed > -0.5f && wheelSpeed < 0.5f)
        {
            car.ShiftToFirstGear();
        }

    }
    private void UpdateSteer()
    {
        car.SteerControl = steerCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * horizontalAxis;
    }

    private void UpdateAutoBreak()
    {
        if (verticalAxis == 0)
        {
            car.BrakeControl = breakCurve.Evaluate(car.WheelSpeed / car.MaxSpeed) * autoBreakStrength;
        }
    }

    private void UpdateAxis()
    {
        verticalAxis = Input.GetAxis("Vertical");
        horizontalAxis = Input.GetAxis("Horizontal");
        handbreakAxis = Input.GetAxis("Jump");
    }
}
