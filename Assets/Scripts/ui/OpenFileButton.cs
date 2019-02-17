using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Windows.Forms;
using System;
using UnityEngine.Events;

public class OpenFileButton : MonoBehaviour
{

    public InputField input_field_path_;

    [Serializable]
    public class OnChoose : UnityEvent<string> { }
    public OnChoose onChoose;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OpenExistFile()
    {

        OpenFileDialog open_file_dialog = new OpenFileDialog();

        //InputFieldの初期値を代入しておく(こうするとダイアログがその場所から開く)
        open_file_dialog.FileName = input_field_path_.text;

        //csvファイルを開くことを指定する
        open_file_dialog.Filter = "vrmファイル|*.vrm";

        //ファイルが実在しない場合は警告を出す(true)、警告を出さない(false)
        open_file_dialog.CheckFileExists = false;

        //ダイアログを開く
        open_file_dialog.ShowDialog();

        //取得したファイル名をInputFieldに代入する
        input_field_path_.text = open_file_dialog.FileName;
        onChoose?.Invoke(open_file_dialog.FileName);
    }

}