using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Defective.JSON;
public class ServerAnimalsDatabase : AnimalsDatabase
{
    [SerializeField] string url;
    public override void PrepareDatas()
    {
        StartCoroutine(DownloadAnimalsDataBase());
    }
    IEnumerator DownloadAnimalsDataBase()
    {
        var webRequest = UnityWebRequest.Get(url);
        yield return webRequest.SendWebRequest();

        var dowloadedText = webRequest.downloadHandler.text;

        var jsonObject = new JSONObject(dowloadedText);
        foreach (var json in jsonObject.list)
        {
            var Name = "";
            json.GetField(ref Name, "name");

            var age = 0;
            json.GetField(ref age, "age");

            var newAnimalsData = new AnimalsData();
            newAnimalsData.name =Name;
            newAnimalsData.age = age;

            AnimalsDataList.Add(newAnimalsData);

        }

    }
}
