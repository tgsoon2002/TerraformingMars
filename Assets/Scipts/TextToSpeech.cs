using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextToSpeech : MonoBehaviour
{
    public InputField _inpufield;
    public AudioSource _audio;
    public Text textField;
    // Use this for initialization
    void Start()
    {

        // _audio = gameObject.GetComponent<AudioSource>();

        //   StartCoroutine(DownloadTheAudio());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator DownloadTheAudio()
    {
        // string textToRead = "Sample";
        string url = "http://translate.google.com/translate_tts?ie=UTF-8&total=1&idx=0&textlen=32&client=tw-ob&q=" + _inpufield.text + "&tl=En-gb";
        string url2 = "https://todolistmongoose.herokuapp.com/tasks/59cb0466efdc3708602ba0bb";
        WWWForm form = new WWWForm();
        form.AddField("name", "first post from unity");
        WWW www = new WWW(url2);
        yield return www;
        textField.text = www.text;

        //_audio.clip = www.GetAudioClip(false, true, AudioType.MPEG);
        //_audio.Play();
    }

    /// <summary>
    /// _ReadText 
    /// </summary
    public void _ReadText()
    {

        StartCoroutine(DownloadTheAudio());
    }



}
