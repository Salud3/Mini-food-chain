using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int cantPollos=2;
    public int cantOveja=2;
    public int cantLobos = 2;
    public int cantOsos = 2;
    public int cantAguilas = 2;
    public int cantCondor = 2;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void SetCantidades(int p, int o, int l, int os, int a, int c)
    {
        cantPollos = p;
        cantOveja = o;  
        cantLobos = l;  
        cantOsos = os;
        cantAguilas = a;
        cantCondor = c;
    }

    
}
