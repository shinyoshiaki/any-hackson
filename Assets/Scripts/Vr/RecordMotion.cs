using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class RecordMotion : MonoBehaviour
{
    public Transform Head, Left, Right;

    public int RecordIndex = 0;
    public List<string>[] MotionRecords = new List<string>[8];
    List<string> MotionRecord = new List<string>();

    private void Start()
    {
        Observable.Interval(TimeSpan.FromSeconds(1 / 30)).Subscribe(_ => Record());
    }

    void Record()
    {
        string head = Head.position.x + "," + Head.position.y + "," + Head.position.z;
        string left = Left.position.x + "," + Left.position.y + "," + Left.position.z;
        string right = Right.position.x + "," + Right.position.y + "," + Right.position.z;
        string position = head + "," + left + "," + right;
        head = Head.rotation.x + "," + Head.rotation.y + "," + Head.rotation.z + "," + Head.rotation.w;
        left = Left.rotation.x + "," + Left.rotation.y + "," + Left.rotation.z + "," + Left.rotation.w;
        right = Right.rotation.x + "," + Right.rotation.y + "," + Right.rotation.z + "," + Right.rotation.w;
        string rotate = head + "," + left + "," + right;
        MotionRecord.Add(position + "," + rotate);
        MotionRecords[RecordIndex] = MotionRecord;
    }

    public void RecordIncr()
    {
        if (RecordIndex == 7) RecordIndex = 0; else RecordIndex++;
        MotionRecord = new List<string>();
    }
}
