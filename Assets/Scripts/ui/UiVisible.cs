using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiVisible : MonoBehaviour
{

    public void Invisible(GameObject go)
    {
        go.SetActive(false);
    }

    public void Visible(GameObject go)
    {
        go.SetActive(true);
    }

    public void SwitchVisible(GameObject go)
    {
        var flag = go.activeSelf;
        go.SetActive(!flag);
    }
}
