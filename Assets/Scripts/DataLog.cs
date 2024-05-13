using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DataLog
{
    public int pollos;
    public int ovejas;
    public int lobos;
    public int osos;
    public int aguilas;
    public int condor;

    DataLog()
    {

    }

    public DataLog(int pollos, int ovejas, int lobos, int osos, int aguilas, int condor)
    {
        this.pollos = pollos;
        this.ovejas = ovejas;
        this.lobos = lobos;
        this.osos = osos;
        this.aguilas = aguilas;
        this.condor = condor;
    }
}
