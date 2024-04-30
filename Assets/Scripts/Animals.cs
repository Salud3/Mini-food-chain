using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals
{
    float sed;
    float fuerza;
    float velocidad;
    float saciedad;
    float vida;
    float vidaMaxima;
    float tiempoVivo;
    float tiempoMuerto;
    bool rabia;
    bool vivo;
    public enum Prio { Agua, Repro, Huir, Comer}
    Prio prio;

    public Animals()
    {

    }
    public Animals(float _sed, float _fuerza, float _velocidad, float _saciedad, float _vidaMaxima, Prio _prio)
    {
        sed = _sed;
        fuerza = _fuerza;
        velocidad = _velocidad;
        saciedad = _saciedad;
        vida = _vidaMaxima;
        vidaMaxima = _vidaMaxima;
        prio = _prio;
        vivo = true;
    }



}
