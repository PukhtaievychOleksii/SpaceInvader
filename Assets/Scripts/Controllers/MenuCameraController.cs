using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraController : MonoBehaviour
{
    [SerializeField]
    private float movingStep;
    [SerializeField]
    private GameObject MainCameraPoint;
    [SerializeField]
    private GameObject LeaderBoardCameraPoint;
    [SerializeField]
    private GameObject OptionsCameraPoint;
    [SerializeField]
    private GameObject ManualCameraPoint;

    private Vector3 destination;

    private void Start()
    {
        destination = MainCameraPoint.transform.position;
    }
    public void GoToLeaderBoard()
    {
        destination = LeaderBoardCameraPoint.transform.position;
    }

    public void GoToMain()
    {
       destination =  MainCameraPoint.transform.position;
    }


    public void GoToManual()
    {
        destination = ManualCameraPoint.transform.position;
    }

    public void GoToOptions()
    {
        destination = OptionsCameraPoint.transform.position;
    }
    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, movingStep);
    }
}
