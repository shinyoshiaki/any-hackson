using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using UnityEngine;

public class Util : MonoBehaviour
{

    public static string Position2string(GameObject go)
    {
        string ans = "";
        ans += (go.transform.position.x + ",");
        ans += (go.transform.position.y + ",");
        ans += (go.transform.position.z);
        return ans;
    }

    public static Vector3 String2Position(string str)
    {
        var arr = str.Split(',');
        int i = 0;
        return new Vector3(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
    }

    public static string Quaternion2String(GameObject go)
    {
        string ans = "";
        ans += (go.transform.rotation.x + ",");
        ans += (go.transform.rotation.y + ",");
        ans += (go.transform.rotation.z + ",");
        ans += (go.transform.rotation.w);
        return ans;
    }

    public static Quaternion String2Quaternion(string str)
    {
        var arr = str.Split(',');
        int i = 0;
        return new Quaternion(float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]), float.Parse(arr[i++]));
    }

    public static string[] divide(byte[] data)
    {
        int length = data.Length / (10000);
        var list = new List<string>();
        foreach (var chunk in data.Chunks(10000))
        {
            var bytes = chunk.ToArray();

            MemoryStream ms = new MemoryStream();
            DeflateStream CompressedStream = new DeflateStream(ms, CompressionMode.Compress, true);
            CompressedStream.Write(bytes, 0, bytes.Length);
            CompressedStream.Close();
            var str = Convert.ToBase64String(ms.ToArray());
            ms.Close();
            list.Add(str);
        }
        return list.ToArray();
    }

    public static byte[] bound(string[] arr)
    {
        var list = new List<byte>();
        foreach (var str in arr)
        {
            var compress = Convert.FromBase64String(str);
            MemoryStream mssrc = new MemoryStream(compress);
            MemoryStream outstream = new MemoryStream();
            byte[] buffer = new byte[1024];
            DeflateStream uncompressStream = new DeflateStream(mssrc, CompressionMode.Decompress);
            while (true)
            {
                int readSize = uncompressStream.Read(buffer, 0, buffer.Length);
                if (readSize == 0) break;
                outstream.Write(buffer, 0, readSize);
            }
            uncompressStream.Close();
            mssrc.Close();
            byte[] outByte = outstream.ToArray();
            foreach (var bit in outByte)
            {
                list.Add(bit);
            }
        }
        return list.ToArray();
    }
}
