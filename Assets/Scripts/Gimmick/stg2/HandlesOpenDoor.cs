using System.Collections.Generic;
using UnityEngine;

public class HandlesOpenDoor : MonoBehaviour
{
    public GameObject parent;

    HandleButtonDoor[] handles;

    public GameObject door;
    [System.NonSerialized]
    public bool death = false;

    private void Start()
    {
        handles = parent.GetComponentsInChildren<HandleButtonDoor>();
    }


    void Update()
    {
        if (death == false)
        {
            bool open = true;
            foreach (HandleButtonDoor handle in handles)
            {
                if (handle.open != true) open = false;
            }
            if (open)
            {
                Debug.Log("handle state");
                door.SetActive(false);
            }
            else
            {
                door.SetActive(true);
            }
        }
    }

    List<string> list = new List<string>();
    public void GhostTouch(string str)
    {
        death = true;
        if (list.Contains(str) == false)
        {
            list.Add(str);
        }
        if (list.Count == handles.Length)
        {
            door.SetActive(false);
        }
        else
        {
            door.SetActive(true);
        }
    }

    public void GhostLeave(string str)
    {
        foreach (string item in list.ToArray())
        {
            if (item == str)
            {
                list.Remove(item);
            }
        }
    }
}
