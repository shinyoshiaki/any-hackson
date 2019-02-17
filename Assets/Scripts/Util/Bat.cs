using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class Bat : MonoBehaviour
{
    static Process process;

    public static void Run(string path, string arg)
    {
        process = new Process();
        process.StartInfo.FileName = path;
        process.StartInfo.Arguments = arg;

        process.EnableRaisingEvents = true;
        process.Exited += Process_Exited;

        process.Start();
    }

    public static void VrHelpRun(string arg)
    {
        var path = Application.streamingAssetsPath + "/bin/bin.exe";
        Run(path, arg);
    }

    static void Process_Exited(object sender, System.EventArgs e)
    {
        //UnityEngine.Debug.Log("Event!");
        process.Dispose();
        process = null;
    }
}
