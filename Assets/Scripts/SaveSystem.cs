using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Unity.VisualScripting;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem Instance;

    [Header("GameManager")]
    public DataLog[] data;
    public AmbientManager manager;

    [Header("Info Player")]
    public int  pollos;
    public int  ovejas;
    public int  lobos;
    public int  osos;
    public int  aguilas;
    public int  condor;

    void Awake()
    {
        Instance = this;
        manager = FindFirstObjectByType<AmbientManager>();
        ReGenInfo();
    }
    private void Start()
    {
        ReadInfo();
        Invoke("Saveall", 1f);
    }

    public void ReadInfo()
    {
        string url = Application.streamingAssetsPath + "/datalog.json";
        string json = File.ReadAllText(url);

        data = JsonHelper.FromJson<DataLog>(json);

        pollos  = data[0].pollos;
        ovejas  = data[0].ovejas;
        lobos   = data[0].lobos;
        osos    = data[0].osos;
        aguilas = data[0].aguilas;
        condor  = data[0].condor;


    }

    public void Saveall()
    {
        AmbientManager.instance.CheckAnimals();

        pollos  = manager.pollos.Count;
        ovejas  = manager.ovejas.Count;
        lobos   = manager.lobos.Count;
        osos    = manager.osos.Count;
        aguilas = manager.aguilas.Count;
        condor  = manager.condor.Count;

        DataLogSave();

        Debug.Log("Salvado");
        Invoke("Saveall", 5f);

    }


    private void DataLogSave()
    {
        if (data.Length<1)
        {
            data = new DataLog[1];
            data[0] = new DataLog(pollos, ovejas, lobos, osos, aguilas, condor);
            
        }
        else
        {
            DataLog[] datatemp = new DataLog[data.Length];
            datatemp = data;
            data = new DataLog[data.Length + 1];
            for (int i = 0; i < data.Length-1; i++)
            {
                data[i] = datatemp[i];
            }
            data[data.Length-1] = new DataLog(pollos, ovejas, lobos, osos, aguilas, condor);
        }

        string json = JsonHelper.ToJson(data, true);
        string url = Application.streamingAssetsPath + "/datalog.json";
        File.WriteAllText(url, json);

        print("Save InfoP " + json);

    }

    public void ReGenInfo()
    {
        data = new DataLog[1];

        data[0] = new DataLog(0, 0, 0, 0, 0, 0);

        string json = JsonHelper.ToJson(data, true);
        string url = Application.streamingAssetsPath + "/datalog.json";
        File.WriteAllText(url, json);

        print(json);

        ReadInfo();

    }


    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper);
        }

        public static string ToJson<T>(T[] array, bool prettyPrint)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}
