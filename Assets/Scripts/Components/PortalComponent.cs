using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalComponent : MonoBehaviour
{
    float upEdge;
    float downEdge = 0;
    float rightEdge;
    float leftEdge = 0;
    void Start()
    {
        upEdge = DataHolder.MainCamera.pixelHeight;
        rightEdge = DataHolder.MainCamera.pixelWidth;
    }

    // Update is called once per frame
    void Update()
    {
        Portal();
    }

    public void Portal()
    {
        Vector3 actorWorldPosition = gameObject.transform.position;
        Vector3 actorScreenPosition = DataHolder.MainCamera.WorldToScreenPoint(actorWorldPosition);
        float newXPosition = actorScreenPosition.x;
        float newYPosition = actorScreenPosition.y;

        if (actorScreenPosition.y > upEdge) newYPosition = downEdge;
        
        if(actorScreenPosition.y < 0) newYPosition = upEdge;

        if (actorScreenPosition.x > rightEdge) newXPosition = leftEdge;

        if (actorScreenPosition.x < leftEdge) newXPosition = rightEdge;

        gameObject.transform.position = DataHolder.MainCamera.ScreenToWorldPoint(new Vector3(newXPosition, newYPosition, actorScreenPosition.z));

    }


}
