using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Controller
{
    private SpaceShip ControlledActor;
    void Start()
    {
        ControlledActor = GetComponent<SpaceShip>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow)) ControlledActor.RestrartAccelerator();
        if (Input.GetKey(KeyCode.UpArrow)) ControlledActor.Accelerate(true);
        if(Input.GetKeyDown(KeyCode.UpArrow)) DataHolder.EffectsHandler.PlaySound(DataHolder.EffectsHandler.RocketSound);
        if (Input.GetKey(KeyCode.DownArrow)) ControlledActor.Accelerate(false);

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) ControlledActor.StopRotate();
        if (Input.GetKeyDown(KeyCode.RightArrow)) ControlledActor.StartRotate(true);
        if (Input.GetKeyDown(KeyCode.LeftArrow)) ControlledActor.StartRotate(false);

        if (Input.GetKeyDown(KeyCode.Space)) ControlledActor.FireForward();

        if (Input.GetKeyDown(KeyCode.F)) ControlledActor.SuperShot();


    }
}
