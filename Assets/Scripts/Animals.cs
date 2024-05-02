using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals
{
    protected float sed;
    protected float sedMax;
    protected float fuerza;
    protected float velocidad;
    protected float saciedad;
    protected float saciedadMax;
    protected float vida;
    protected float vidaMaxima;
    protected float tiempoVivo;
    protected float tiempoMuerto;
    protected bool rabia;
    protected bool vivo;
    public enum Prio { Agua =0, Repro=1, Huir=2, Comer=3}
    protected Prio prio;

    public Animals()
    {

    }
    public Animals(float _sed, float _fuerza, float _velocidad, float _saciedad, float _vidaMaxima, Prio _prio)
    {
        sed = _sed;
        sedMax = _sed;
        fuerza = _fuerza;
        velocidad = _velocidad;
        saciedad = _saciedad;
        saciedadMax = _saciedad;
        vida = _vidaMaxima;
        vidaMaxima = _vidaMaxima;
        prio = _prio;
        vivo = true;
    }

    public void Rabiar(bool rabiar)
    {
        this.rabia = rabiar;

    }
    public void Deshidratar(float sed)
    {
        this.sed -= sed;
    }
    public void Hambre(float hambre)
    {
        this.saciedad -= hambre;
    }
    public void Comer(float hambre)
    {
        this.saciedad += hambre;
    }
     
}
