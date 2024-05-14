using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Aguila : Animals
{
    [Header("Variables Posicionales")]
    public Movement movement;//Script de Movimiento
    public GameObject objetive; // A donde se dirije
    public GameObject Pareja;// variable para tener una pareja para reproducirse

    [Header("Requerimentos")]
    public int id = 4;//ID del tipo de animal
    public bool comiendo;//usado para comer o beber
    public bool reproducido;//ya se ha reproducido?
    public Vector3 crecimiento;//vector que crecera cada 15 segundos
    bool distanceRrepro;

    public float timeAlive;

    private void Start()//inicializamos al animal
    {
        timeAlive = Random.Range(50f, 300f);
        genes = new Gen(0);
        movement = GetComponent<Movement>();
        genes.vida = genes.vidaMaxima;
        genes.saciedad = genes.saciedadMax;
        genes.sed = genes.sedMax;
        movement.velMov = genes.velocidad;
        vivo = true;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.name = gameObject.name + AmbientManager.instance.IDGenerator().ToString();
        Invoke("Nescesidades", 10f);
        Invoke("Crecer", 15f);
    }
    private void FixedUpdate()
    {
        if (vivo)
        {
            tiempoVivo += Time.deltaTime;
            if (tiempoVivo > timeAlive || genes.vida <= 0)
            {
                if (Random.Range(-5, 5) == 0)
                {
                    vivo = false;
                    transform.rotation = new Quaternion(90, 0, 0, 0);
                    GetComponent<Rigidbody>().useGravity = true;
                    GetComponent<Movement>().enabled = false;
                }
            }
        }
        else
        {
            tiempoMuerto += Time.deltaTime;

            if (tiempoMuerto > 120)
            {
                GetComponent<Collider>().isTrigger = true;
                Destroy(this.gameObject);
            }
        }

        distanceRrepro = movement.distanceRrepro;
    }


    void Crecer()//Desarrollamos al animalito
    {
        if (transform.localScale.x < 1 && vivo)
        {
            transform.localScale += crecimiento;
        }


        Invoke("Crecer", 15f);
    }

    public void Nescesidades()
    {
        Hambre(1);
        Deshidratar(1);

        switch (genes.prio)
        {
            case Prio.Agua:


                objetive = AmbientManager.instance.BuscarAgua();
                BuscarComida(objetive.transform);

                break;
            case Prio.Repro:

                if (Pareja == null)
                {
                    BuscarPareja();
                }
                else if (Pareja != null && !distanceRrepro)
                {
                    Reproduccion();
                }
                else if (Pareja != null && distanceRrepro)
                {
                    Heredar();
                }

                break;
            case Prio.Huir:

                break;
            case Prio.Comer:
                objetive = AmbientManager.instance.BuscarPasto();
                BuscarComida(objetive.transform);
                break;
            case Prio.NONE:

                break;

            default:
                break;
        }

        int rand = Random.Range(1, 15);
        Invoke("Nescesidades", rand);
    }

    public override void Beber(float sed)
    {
        genes.sed += sed;
    }

    public override void Comer(float hambre)
    {
        genes.saciedad += hambre;
    }

    public override void GetDamage(float damage)
    {
        genes.vida -= damage;
    }

    public override void Deshidratar(float sed)
    {
        genes.sed -= sed;
    }


    public override void Hambre(float hambre)
    {
        genes.saciedad -= hambre;
    }


    public override void BuscarComida(Transform position)
    {
        movement.AssingObjetive(position);
    }


    public override void BuscarPareja()
    {
        Pareja = AmbientManager.instance.BuscarPareja(id, 0, this);
        if (Pareja != null)
        {
            Debug.Log(this);
            Reproduccion();
            Pareja.GetComponent<Aguila>().Reproduccion();
        }
    }

    public override void Rabiar(bool rabiar)
    {
        this.rabia = rabiar;
    }
    public void Heredar()
    {
        if (distanceRrepro && !reproducido)
        {
            AmbientManager.instance.InitRepro(this, Pareja.GetComponent<Aguila>(), id);
        }
    }
    public void Reproduccion()
    {
        objetive = Pareja;
        movement.AssingObjetive(Pareja.transform);
        movement.instintos = true;
        movement.ChangeState(Movement.States.REPRO);
    }


    float RegladeTres(float stat, float maxstat)
    {
        return (stat * 100) / maxstat;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Water") && RegladeTres(genes.sed, genes.sedMax) < (genes.sedMax * .60f))
        {
            movement.ChangeState(Movement.States.EATKING);
            movement.instintos = true;
            Beber(Time.deltaTime / 2);
        }

        if (other.transform.CompareTag("Grass") && RegladeTres(genes.saciedad, genes.saciedadMax) < (genes.saciedadMax * .60f))
        {
            movement.ChangeState(Movement.States.EATKING);
            movement.instintos = true;
            Comer(Time.deltaTime / 2);
        }

        if (other.transform.CompareTag("Animal"))
        {
            movement.ChangeState(Movement.States.RUNNING);
            movement.instintos = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Water"))
        {
            movement.instintos = false;
            movement.Accion();

        }

        if (other.transform.CompareTag("Grass"))
        {
            movement.instintos = false;
            movement.Accion();

        }


        if (other.transform.CompareTag("Animal"))
        {
            movement.instintos = false;
            movement.Accion();

        }
    }


}
