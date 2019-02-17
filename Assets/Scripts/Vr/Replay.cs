using FileHelper;
using System;
using UniRx;
using UnityEngine;
using VRM;

public class Replay : MonoBehaviour
{
    public RecordMotion record;


    struct GhostPos
    {
        public Transform head, left, right;
    }

    public async void Play()
    {
        record.RecordIncr();

        var path = Application.streamingAssetsPath + "/" + "host.vrm";
        var bytes = await new FileRead().ReadAllBytesAsync(path);
        var context = new VRMImporterContext();
        context.ParseGlb(bytes);
        var meta = context.ReadMeta(false);
        Debug.LogFormat("meta: title:{0}", meta.Title);
        await context.LoadAsyncTask();

        var go = context.Root;
        go.transform.SetParent(transform, false);
        go.transform.position = new Vector3(0, 0, 0);
        context.ShowMeshes();
        var pos = SetupVRIK(go);

        int progress = 0;
        int index = record.RecordIndex;
        Observable.Interval(TimeSpan.FromSeconds(1 / 60)).Subscribe(_ => { if (progress >= 0) { progress = run(pos, progress, index); } });
    }


    int run(GhostPos ghostPos, int progress, int index)
    {
        if (index == 0)
        {
            Debug.Log("no record");
            return -1;
        };
        var motion = record.MotionRecords[index - 1];
        if (motion.Count - 35 <= progress)
        {
            Debug.Log("motion end");
            return -1;
        }

        var pose = motion[progress++];
        var arr = pose.Split(',');
        int i = 0;
        ghostPos.head.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
        ghostPos.left.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
        ghostPos.right.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
        ghostPos.head.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
        ghostPos.left.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
        ghostPos.right.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));

        return progress;
    }


    GhostPos SetupVRIK(GameObject avatar)
    {
        GameObject headTarget, leftHandTarget, rightHandTarget;
        headTarget = new GameObject();
        leftHandTarget = (GameObject)Instantiate(Resources.Load("hand"), new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        rightHandTarget = (GameObject)Instantiate(Resources.Load("hand"), new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        GhostPos ghostPos = new GhostPos();
        ghostPos.head = headTarget.transform;
        ghostPos.left = leftHandTarget.transform;
        ghostPos.right = rightHandTarget.transform;
        headTarget.transform.position = new Vector3(0f, 1.5f, 0f);
        leftHandTarget.transform.position = new Vector3(-0.5f, 0.8f, 0f);
        rightHandTarget.transform.position = new Vector3(0.5f, 0.8f, 0f);
        var vrIK = avatar.AddComponent<RootMotion.FinalIK.VRIK>();
        vrIK.AutoDetectReferences();

        // NullReferenceエラーがでるので初期化しておく
        vrIK.solver.leftArm.stretchCurve = new AnimationCurve();
        vrIK.solver.rightArm.stretchCurve = new AnimationCurve();

        // 頭や腕のターゲット設定
        vrIK.solver.spine.headTarget = headTarget.transform;
        vrIK.solver.leftArm.target = leftHandTarget.transform;
        vrIK.solver.rightArm.target = rightHandTarget.transform;

        // 歩幅の設定            
        vrIK.solver.locomotion.footDistance = 0.1f;

        return ghostPos;
    }
}
