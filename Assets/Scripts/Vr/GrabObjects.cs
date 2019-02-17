using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;
using VRTK.GrabAttachMechanics;

public class GrabObjects : MonoBehaviour
{

    void Awake()
    {
        var children = GetAllChildren.GetAll(gameObject);
        foreach (var child in children)
        {
            AddGrabAble(child, true);
        }
    }

    public static  void AddGrabAble(GameObject go, bool gravity)
    {
        var interact = go.AddComponent<VRTK_InteractableObject>();
        interact.isGrabbable = true;
        interact.isUsable = true;
        var fixedjoin = go.AddComponent<VRTK_FixedJointGrabAttach>();
        fixedjoin.precisionGrab = true;
        var rigid = go.AddComponent<Rigidbody>();
        if (!gravity)
        {
            rigid.isKinematic = true;
        }
    }

}
