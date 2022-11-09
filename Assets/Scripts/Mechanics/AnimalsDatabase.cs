using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimalsDatabase : MonoBehaviour
{
    [SerializeField] protected List<AnimalsData> AnimalsDataList = new List<AnimalsData>();
    protected AnimalsData defaultText;

    private void Awake()
    {
        PrepareDatas();
    }
    public abstract void PrepareDatas();

    public AnimalsData GetDefaultData()
    {
        return defaultText;
    }
    public AnimalsData GetAnimalsDataByName(string name)
    {
        foreach (var AnimalsData in AnimalsDataList)
        {
            if (name.Contains(AnimalsData.name))
                return AnimalsData;
        }
        return null;
    }

   

}
