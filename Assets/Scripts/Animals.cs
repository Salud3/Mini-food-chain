using UnityEngine;

public interface IAnimalBehaviour
{
    abstract void Deshidratar(float sed);
    abstract void DoDamage(float damage);
    abstract void GetDamage(float damage);
    abstract void Comer(int comida);
    abstract void Rabiar(bool rabiar);
    abstract void BuscarComida(Transform position);
    abstract void BuscarPareja(Animals pareja);
    abstract void Beber(float sed);//futura implementacion de veneno
    abstract void Hambre(float hambre);
}
public enum Prio { Agua = 0, Repro = 1, Huir = 2, Comer = 3 }


public abstract class Animals : MonoBehaviour, IAnimalBehaviour
{
    public Gen genes;
    public float tiempoVivo;
    public float tiempoMuerto;
    public bool rabia;
    public bool vivo;
    

    public Animals() { }
    public abstract void DoDamage(float damage);
    public abstract void GetDamage(float damage);
    public abstract void Rabiar(bool rabiar);
    public abstract void Deshidratar(float sed);
    public abstract void BuscarComida(Transform position);
    public abstract void BuscarPareja(Animals pareja);
    public abstract void Beber(float sed);            //if (veneno){Comenzar deterioro}
    public abstract void Hambre(float hambre);
    public abstract void Comer(int hambre);
     
}
[System.Serializable]
public class Gen
{
    //Stats Base
    public float sedMax;
    public float fuerza;
    public float velocidad;
    public float saciedadMax;
    public float vidaMaxima;
    public Prio prio;

    //Variables
    public float sed;
    public float vida;
    public float saciedad;

    public Gen(){ }

    public Gen(int id)
    {
        sedMax = Random.Range(1, 50);
        velocidad = Random.Range(1, 50);
        fuerza = Random.Range(1, 50);
        saciedadMax = Random.Range(1, 50);
        vidaMaxima = Random.Range(1, 50);
        prio = (Prio)Random.Range(0, 3);
    }

    public Gen(float _sedMax, float _velocidad, float _fuerza, float _saciedadMax, float _vidaMaxima, Prio _prio)
    {
        sedMax = _sedMax;
        velocidad = _velocidad;
        fuerza = _fuerza;
        saciedadMax = _saciedadMax;
        vidaMaxima = _vidaMaxima;
        prio = _prio;
    }

    public void Mutate(float probability)
    {
        if (Random.Range(0f, 1f) > probability)
        {
            sedMax += Random.Range(-20f, 20f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            velocidad += Random.Range(-35f, 35f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            fuerza += Random.Range(-40f, 40f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            saciedadMax += Random.Range(-20f, 20f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            vidaMaxima += Random.Range(-50f, 50f);
        }

        if (Random.Range(0f, 1f) > probability)
        {
            prio = (Prio)Random.Range(0, 4);
        }
    }


    public Gen Reproduce(Gen g2)
    {
        Gen gHijo = new Gen(sedMax, velocidad, fuerza, g2.saciedadMax, g2.vidaMaxima, g2.prio);
        gHijo.Mutate(Random.Range(0f, 1f));
        return gHijo;
    }


}


