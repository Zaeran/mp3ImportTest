using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using NLayer;

public class ImportAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AudioImport());
    }

    IEnumerator AudioImport()
    {
        Debug.Log("Starting import");
        UnityWebRequest www = UnityWebRequest.Get("D:/Temp/Test.mp3");
        yield return www.SendWebRequest();
        Debug.Log("File obtained");
        byte[] results = www.downloadHandler.data;
        var memStream = new System.IO.MemoryStream(results);
        var mpgFile = new NLayer.MpegFile(memStream);
        var samples = new float[mpgFile.Length];

        mpgFile.ReadSamples(samples, 0, (int)mpgFile.Length);
        Debug.Log("Samples read");
        var clip = AudioClip.Create("foo", samples.Length, mpgFile.Channels, mpgFile.SampleRate, false);
        clip.SetData(samples, 0);
        Debug.Log("Clip created");
        GetComponent<AudioSource>().clip = clip;
        GetComponent<AudioSource>().Play();
        Debug.Log("Playing audio");
    }
        
}
