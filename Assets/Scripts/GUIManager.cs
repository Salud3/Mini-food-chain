using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    
    public TMP_InputField aguilas;
    public TMP_InputField pollos;
    public TMP_InputField Ovejas;
    public TMP_InputField Osos;
    public TMP_InputField Lobos;
    public TMP_InputField Condor;
    public int cantPollos = 0;
    public int cantOveja = 0;
    public int cantLobos = 0;
    public int cantOsos = 0;
    public int cantAguilas = 0;
    public int cantCondor = 0;

    public void SetPollos(string pollos)
    {
        if (pollos != null)
        {
            cantPollos = int.Parse(pollos);
        }
        else
        {
            cantPollos = 0;
        }

        GameManager.Instance.cantPollos = cantPollos;
        Debug.Log(pollos);
    }
    public void SetOvejas(string ovejas)
    {
        if (ovejas != null)
        {
        cantOveja = int.Parse(ovejas);
        }
        else
        {
            cantOveja= 0;
        }

        GameManager.Instance.cantOveja = cantOveja;
        Debug.Log(ovejas);
    }
    public void SetLobos(string lobos)
    {
        if (lobos != null)
        {
        cantLobos = int.Parse(lobos);
        }
        else
        {
            cantLobos= 0;
        }

        GameManager.Instance.cantLobos = cantLobos;
        Debug.Log(cantLobos);
    }
    public void SetOsos(string osos)
    {
        if (osos != null)
        {
        cantOsos = int.Parse(osos);
        }
        else
        {
            cantOsos=0;
        }

        GameManager.Instance.cantOsos = cantOsos;
        Debug.Log(cantOsos);
    }
    public void SetAguilas(string agui)
    {
        if (agui != null)
        {
        cantAguilas = int.Parse(agui);
        }
        else
        {
            cantAguilas= 0;
        }

        GameManager.Instance.cantAguilas = cantAguilas;
        Debug.Log(cantAguilas);
    }
    public void SetCondor(string condor)
    {
        if (condor != null)
        {
            cantCondor = int.Parse(condor);
        }
        else
        {
            cantCondor=0;
        }

        GameManager.Instance.cantCondor = cantCondor;
        Debug.Log(cantPollos);
    }

    public void Play()
    {
        pollos.enabled = false;
        Ovejas.enabled = false;
        Lobos.enabled = false;
        Osos.enabled = false;
        aguilas.enabled = false;
        Condor.enabled = false;

        GameManager.Instance.SetCantidades(cantPollos,cantOveja,cantLobos,cantOsos,cantAguilas,cantCondor);

        SceneManager.LoadScene(1);

    }



}
