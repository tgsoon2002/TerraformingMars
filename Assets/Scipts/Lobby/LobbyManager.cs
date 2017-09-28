using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : MonoBehaviour
{
    public InputField _inpufield;
    public AudioSource _audio;
    public Text textField;
    string urlLink = "https://todolistmongoose.herokuapp.com/";
    string result;


    // Use this for initialization
    void Start()
    {

    }

    IEnumerator DownloadTheAudio()
    {
        //WWWForm form = new WWWForm();
        //form.AddField("name", "first post from unity");

        WWW www = new WWW(urlLink + "tasks/");
        yield return www;
        result = www.text;
    }

    /// <summary>
    /// _ReadText 
    /// </summary
    public void _FindList()
    {
        StartCoroutine(DownloadTheAudio());
    }
}