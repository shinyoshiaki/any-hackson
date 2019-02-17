using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VrUtil : MonoBehaviour {

  public static  void SetupLipSync(GameObject go)
    {
        var lipSyncContext = go.AddComponent<OVRLipSyncContext>();
        var ovrMic = go.AddComponent<OVRLipSyncMicInput>();
        ovrMic.micControl = OVRLipSyncMicInput.micActivation.ConstantSpeak;
    }
}
