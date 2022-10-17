using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Defective.JSON;
public class LocalAnimalsDatabase: AnimalsDatabase
{
    [SerializeField] TextAsset jsonFile;

    public override void PrepareDatas()
    {
        var jsonObject = new JSONObject(jsonFile.text);
        foreach (var json in jsonObject.list)
        {
            var Name = "";
            json.GetField(ref Name, "name");

            var age =0 ;
            json.GetField(ref age, "age");

            var newAnimalsData = new AnimalsData();
            newAnimalsData.name = Name;
            newAnimalsData.age = age;

            AnimalsDataList.Add(newAnimalsData);

        }

    }
}
