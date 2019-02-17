using UnityEngine;
using VRTK;

public class ForceWarp : VRTK_DestinationMarker
{
    public Transform destination;

    public VRTK_ControllerEvents controller;

    public void Warp()
    {
        Debug.Log("force move");
        float distance = Vector3.Distance(transform.position, destination.position);
        VRTK_ControllerReference controllerReference = VRTK_ControllerReference.GetControllerReference(controller.gameObject);
        OnDestinationMarkerSet(SetDestinationMarkerEvent(distance, destination, new RaycastHit(), destination.position, controllerReference));
    }
}
