using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpentheDoor : MonoBehaviour
{
    Animator animator;
    GameObject door;
    new Collider collider;

    void Start()
    {
       // GameObject door = GameObject.Find("door");
        animator = GetComponent(typeof(Animator)) as Animator;
        collider = GetComponent(typeof(Collider)) as Collider;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            animator.Play("DoorAOpen");
            collider.isTrigger = true;
            Debug.Log("a");
        }
    }
}
