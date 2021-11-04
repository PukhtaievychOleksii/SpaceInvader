using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : Controller
{
    private Alien ControlledActor;
    void Start()
    {
        ControlledActor = GetComponent<Alien>();
        ControlledActor.target = DataHolder.SpaceShip;
        ControlledActor.StartPermanentFireing();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
