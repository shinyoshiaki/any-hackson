using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Browser : MonoBehaviour {

    public static void Open(string url)
    {
        string prev = "<html><head><script type=\"text/javascript\">function load(){window.location.href = \"";
        string next = "\";}</script></head><body onload=\"load()\"></body></html>";

        string body = prev + url + next;
        Debug.Log("body " + body);
        string local = Application.streamingAssetsPath + "/index.html";
        File.WriteAllText(local, body);
        Debug.Log("local pdf " + local);
        Application.OpenURL(local);
    }
}
