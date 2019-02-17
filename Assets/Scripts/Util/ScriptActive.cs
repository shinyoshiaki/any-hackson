using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptActive : MonoBehaviour
{

    public void ActiveDelay()
    {
        var cor = StartCoroutine(DelayMethod(2.0f, () => { var components = GetComponents<Component>();
            foreach(var component in components)
            {
                //component
            }
        }));
    }

    private IEnumerator DelayMethod(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action();
    }
}
