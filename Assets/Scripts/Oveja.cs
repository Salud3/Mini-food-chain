using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Oveja : MonoBehaviour
{
    public OvejaC Stats;

    public GameObject objetive;
    public GameObject Pareja;

    public NavMeshAgent agent;

    public float tiempoVivo;
    public float tiempoMuerto;
    public float tiempodesdeGen;
    //OVEJA ES CLASE 1
    private void Start()
    {
        Stats = (OvejaC)AmbientManager.instance.GenerarStats(1);
        Pareja = AmbientManager.instance.Emparejar(1);
        Stats.disp = true;
        StartCoroutine(BuscarComida());
        StartCoroutine(CheckPareja());
    }

    private void Update()
    {
        if (Stats.Vivo)
        {
            tiempoVivo += Time.deltaTime;
            tiempodesdeGen += Time.deltaTime;
            Stats.Hambre(Time.deltaTime * 1/ 2);
            Stats.Deshidratar(Time.deltaTime * 1/ 2);
            if (!Stats.disp)
            {
                agent.SetDestination(Pareja.transform.position);
            }
        }
        else { 

        tiempoMuerto += Time.deltaTime;
        }
    }

    IEnumerator CheckStats()
    {
        switch (Stats.Prior)
        {
            case Animals.Prio.Agua:
                break;
            case Animals.Prio.Repro:
                break;
            case Animals.Prio.Huir:
                break;
            case Animals.Prio.Comer:
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(15);
    }
    IEnumerator CheckPareja()
    {
        yield return new WaitForSeconds(05);
        if (Stats.disp)
        {
            StartCoroutine(CheckPareja());
        }
        else
        {
            Pareja = AmbientManager.instance.Emparejar(1);
            StartCoroutine(CheckPareja());

        }
    }
    IEnumerator BuscarComida()
    {
        if (Stats.Rabioso)
        {
            objetive = AmbientManager.instance.RabiaObjective();
            if (objetive == null|| objetive == this)
            {
                objetive = AmbientManager.instance.RabiaObjective();
            }
            yield return new WaitForSeconds(50);
            BuscarComida();
        }
        else
        {
            objetive = AmbientManager.instance.Objetives(1);
            yield return new WaitForSeconds(5);
            BuscarComida();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pasto" && Stats.disp)
        {
            StartCoroutine(Comer());
        }
    }
    bool comiendo = false;
    IEnumerator Comer()
    {
        Stats.Comer(3);
        yield return new WaitForSeconds(2);

    }
    
    IEnumerator Aparear()
    {
        yield return new WaitForSeconds(200);
        if (tiempodesdeGen >350)
        {
            Stats.disp = false;
            StopAllCoroutines();
        }

    }
}

public class OvejaC : Animals
{
    public float Sed { get { return sed; } set {sed = value; } }
    public float Fuerza { get { return fuerza; } set { fuerza = value; } }
    public float Velocidad { get { return velocidad; } set { velocidad = value; } }
    public float Saciedad { get { return saciedad; } set { saciedad = value; } }
    public float Vida { get { return vida; } set { vida = value; } }
    public bool Rabioso { get { return rabia; } set { rabia = value; } }
    public bool Vivo { get { return vivo; } set { vivo = value; } }

    public Prio Prior { get { return prio; }} 
    
    public bool disp;//esta con pareja

    

}
