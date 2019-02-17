using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[Serializable]
public class MainState
{
    public bool downloded = false;
    public bool down_comp = false;
    public bool firstDownloaded = true;
}


public class Main : MonoBehaviour
{
    public Transform Head, Left, Right;
    public Text LodingText;
    public InputField VrmPathText;
    public bool UnityOpusVoice = false;  

    [NonSerialized]
    public static GameObject Guest = null;

    struct GuestPos
    {
        public Transform head, left, right;
    }
    GuestPos guestPos = new GuestPos();


    public MainState state = new MainState();

    List<string> VrmBase64 = new List<string>();
    public async void OnReceived(string s)
    {
        /*switch (data.value[0])
        {
            case "VRM":
                if (data.value[1] == "bin")
                {
                    //Debug.Log("vrm bin " + data.value[2]);
                    VrmBase64.Add(data.value[2]);
                    LodingText.gameObject.SetActive(true);
                    LodingText.text = data.value[3] + "%";
                }
                else if (data.value[1] == "fin")
                {
                    Debug.Log("vrm downloaded");
                    var bytes = Util.bound(VrmBase64.ToArray());
                    var vrm = await VRMImporter.LoadVrmAsync(bytes);
                    vrm.transform.position = new Vector3(0, 0, 1);
                    VrmBase64 = new List<string>();
                    SetupVRIK(vrm);
                    VrUtil.SetupLipSync(vrm);

                    Guest = vrm;

                    LodingText.gameObject.SetActive(false);
                    state.downloded = true;
                    //DownloadCompSend();

                   // if (!isHost) SendVrm();
                }
                else if (data.value[1] == "comp")
                {
                    Debug.Log("download comp");
                    state.down_comp = true;
                }
                break;
            case "MOTION":
                if (guestPos.head != null)
                {
                    var arr = data.value[1].Split(',');
                    int i = 0;
                    guestPos.head.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
                    guestPos.left.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
                    guestPos.right.position = new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
                    guestPos.head.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
                    guestPos.left.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
                    guestPos.right.rotation = new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));

                }
                break;
        }
        if (state.downloded && state.down_comp && state.firstDownloaded)
        {
            Debug.Log("downloaded");
            state.firstDownloaded = false;
            downloadedAction?.Invoke();
            Observable.Interval(TimeSpan.FromSeconds(1 / 30))
                .Subscribe(_ => SendMotion());
        }*/
    }

    GameObject headTarget, leftHandTarget, rightHandTarget;
    public void OnConnect()
    {
        headTarget = new GameObject();
        leftHandTarget = new GameObject();
        rightHandTarget = new GameObject();

        //if (isHost) SendVrm();
    }

    void SendMotion()
    {
        if (Head == null) return;

        string head = Head.position.x + "," + Head.position.y + "," + Head.position.z;
        string left = Left.position.x + "," + Left.position.y + "," + Left.position.z;
        string right = Right.position.x + "," + Right.position.y + "," + Right.position.z;
        string position = head + "," + left + "," + right;
        head = Head.rotation.x + "," + Head.rotation.y + "," + Head.rotation.z + "," + Head.rotation.w;
        left = Left.rotation.x + "," + Left.rotation.y + "," + Left.rotation.z + "," + Left.rotation.w;
        right = Right.rotation.x + "," + Right.rotation.y + "," + Right.rotation.z + "," + Right.rotation.w;
        string rotate = head + "," + left + "," + right;
        //Debug.Log("sendMotion " + position);
        
    }

    void SetupVRIK(GameObject avatar)
    {
        guestPos.head = headTarget.transform;
        guestPos.left = leftHandTarget.transform;
        guestPos.right = rightHandTarget.transform;
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
    }
}
