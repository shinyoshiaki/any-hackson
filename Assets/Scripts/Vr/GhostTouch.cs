using UnityEngine;

public class GhostTouch : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "ButtonStg1")
        {
            GameObject gameObject = GameObject.Find("open_door");
            HandlesOpenDoor handle = gameObject.GetComponent<HandlesOpenDoor>();
            handle.GhostTouch(other.gameObject.GetInstanceID().ToString());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "ButtonStg1")
        {
            GameObject gameObject = GameObject.Find("open_door");
            HandlesOpenDoor handle = gameObject.GetComponent<HandlesOpenDoor>();
            handle.GhostLeave(other.gameObject.GetInstanceID().ToString());
        }
    }

}
