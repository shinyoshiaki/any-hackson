using FileHelper;
using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using VRM;

public class Player : MonoBehaviour
{
    public GameObject left, right, head;
    GameObject go;
    public InputField pathField;
    public ForceWarp ForceWarp;

    void Start()
    {
        var path = Application.streamingAssetsPath + "/" + "host.vrm";
        Debug.Log("vrm path " + path);
        pathField.text = path;
        Load(path);
    }

    public void LoadFromGui(InputField text)
    {
        Load(text.text);
    }

    public async void Load(string path)
    {
        Debug.Log("load" + path);
        var bytes = await new FileRead().ReadAllBytesAsync(path);
        Destroy(go);
        var context = new VRMImporterContext();
        context.ParseGlb(bytes);
        var meta = context.ReadMeta(false);
        Debug.LogFormat("meta: title:{0}", meta.Title);
        await context.LoadAsyncTask();

        go = context.Root;
        go.transform.SetParent(transform, false);
        go.transform.position = new Vector3(0, 0, 0);
        context.ShowMeshes();
        SetupVRIK(go);
        SetupLipSync(go);
        left.tag = "Player";
        right.tag = "Player";
        ForceWarp.Warp();
    }

    void SetupVRIK(GameObject avatar)
    {
        var vrIK = avatar.AddComponent<RootMotion.FinalIK.VRIK>();
        vrIK.AutoDetectReferences();

        // NullReferenceエラーがでるので初期化しておく
        vrIK.solver.leftArm.stretchCurve = new AnimationCurve();
        vrIK.solver.rightArm.stretchCurve = new AnimationCurve();

        // 頭や腕のターゲット設定
        vrIK.solver.spine.headTarget = head.transform;
        vrIK.solver.leftArm.target = left.transform;
        vrIK.solver.rightArm.target = right.transform;

        // 歩幅の設定            
        vrIK.solver.locomotion.footDistance = 0.1f;
    }

    bool isConnect = false;
    void SetupLipSync(GameObject go)
    {
        var lipSyncContext = go.AddComponent<OVRLipSyncContext>();
        var ovrMic = go.AddComponent<OVRLipSyncMicInput>();
        ovrMic.micControl = OVRLipSyncMicInput.micActivation.ConstantSpeak;
        var blendShapeProxy = go.GetComponent<VRMBlendShapeProxy>();

        Observable.Interval(TimeSpan.FromSeconds(1 / 60)).Subscribe(_ =>
        {
            var now = lipSyncContext.GetCurrentPhonemeFrame();
            blendShapeProxy.ImmediatelySetValue(BlendShapePreset.A, now.Visemes[(int)OVRLipSync.Viseme.aa]);
            blendShapeProxy.ImmediatelySetValue(BlendShapePreset.I, now.Visemes[(int)OVRLipSync.Viseme.ih]);
            blendShapeProxy.ImmediatelySetValue(BlendShapePreset.U, now.Visemes[(int)OVRLipSync.Viseme.ou]);
            blendShapeProxy.ImmediatelySetValue(BlendShapePreset.E, now.Visemes[(int)OVRLipSync.Viseme.E]);
            blendShapeProxy.ImmediatelySetValue(BlendShapePreset.O, now.Visemes[(int)OVRLipSync.Viseme.oh]);
            blendShapeProxy.Apply();
        }).AddTo(this);
        Observable.Interval(TimeSpan.FromSeconds(1 / 10)).Subscribe(_ =>
          {
              var now = lipSyncContext.GetCurrentPhonemeFrame();
          }).AddTo(this);
    }

    class RipSyncSendJson
    {
        public float a, i, u, e, o;
    }

    public void StartSend()
    {
        isConnect = true;
    }


}
