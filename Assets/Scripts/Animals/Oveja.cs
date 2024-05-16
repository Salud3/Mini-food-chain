using System.Collections;
using System.ComponentModel;
using UnityEngine;

public class Oveja : Animals
{
    [Header("Variables Posicionales")]
    public Movement movement;//Script de Movimiento
    public GameObject objetive; // A donde se dirije
    public GameObject Pareja;// variable para tener una pareja para reproducirse
    [Header("Requerimentos")]
    public int id = 1;//ID del tipo de animal
    public bool comiendo;//usado para comer o beber
    public bool reproducido;//ya se ha reproducido?
    public Vector3 crecimiento;//vector que crecera cada 15 segundos
    bool distanceRrepro;

    public float timeAlive;

    private void Start()//inicializamos al animal
    {
        timeAlive = Random.Range(150f, 600f);
        
        genes = new Gen(0);
        
        movement = GetComponent<Movement>();
        
        SetGenesVar();

        Invoke("Crecer", 15f);
    }

    public void SetGenesVar()
    {
        genes.vida = genes.vidaMaxima;
        genes.saciedad = genes.saciedadMax;
        genes.sed = genes.sedMax;
        movement.velMov = genes.velocidad;

        vivo = true;
        transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.name = gameObject.name + AmbientManager.instance.IDGenerator().ToString();


    }
    private void FixedUpdate()
    {
        if (genes.vida <= 0)
        {
            vivo = false;
            GetComponent<Rigidbody>().useGravity = true;
            GetComponent<Movement>().enabled = false;
        }
        if (vivo)
        {
            tiempoVivo += Time.deltaTime;
            if (tiempoVivo > timeAlive || genes.vida <= 0 )
            {
                if (Random.Range(-5, 5) == 0)
                {
                    vivo = false;
                    transform.rotation = new Quaternion(90,0,0,0);
                    GetComponent<Rigidbody>().useGravity = true;
                    transform.GetComponent<Collider>().isTrigger = true;
                    GetComponent<Movement>().enabled = false;
                }
            }
        }
        else
        {
            tiempoMuerto += Time.deltaTime;
            transform.GetComponent<Collider>().isTrigger = true;


            if (tiempoMuerto > 60)
            {
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


        int ran = Random.Range(0, 4);
        if (ran == 1)
        {
            ChangeState((Prio)Random.Range(0, 3));
        }

        fuzzyLogic();

        Hambre(1);
        Deshidratar(1);

        Invoke("Crecer", 15f);
    }

    public void fuzzyLogic()
    {
        float fuzzySed = RegladeTres(genes.sed, genes.sedMax);
        float fuzzySac = RegladeTres(genes.saciedad, genes.saciedadMax);
        float fuzzyvid = RegladeTres(genes.vida, genes.vidaMaxima);
        bool awa = false;

        switch (genes.prio)
        {
            case Prio.Agua:

                if (fuzzySed < 15)
                {
                    awa = true;
                }
                else if (fuzzySed > 75)
                {
                    CheckPrio();
                }
                break;
            case Prio.Repro:
                
                if (reproducido)
                {
                    awa = true;
                }
                else
                {
                    CheckPrio();
                }

                break;
            case Prio.Huir:
                if (fuzzyvid < 85)
                {
                    awa = true;
                }
                else if (fuzzyvid > 75)
                {
                    CheckPrio();
                }
                break;
            case Prio.Comer:
                if (fuzzySac < 15)
                {
                    awa = true;
                }
                else if (fuzzySac > 75)
                {
                    CheckPrio();
                }
                break;
            case Prio.NONE:
                awa = true;
                break;
            default:
                break;
        }

        if (awa)
        {
            Priochecked(fuzzySac,fuzzySed,fuzzyvid);
        }


    }

    int priochange = 0;
    
    public void Priochecked(float saciedad, float sed, float vida)
    {
        if (priochange < 4)
        {
            if (genes.prio != Prio.Comer && saciedad < 55)
            {
                genes.prio = Prio.Comer;
            }
            else if( genes.prio != Prio.Agua && sed < 45)
            {
                genes.prio = Prio.Agua;
            }
            else if (vida < 45)
            {
                genes.prio = Prio.Huir;
            }
            else
            {
                priochange++;
                Debug.Log(gameObject.name + " " + genes.prio.ToString() +" - Prioridad Actual: no nescesaria considerando reproduccion" + priochange);
            }

        }
        else
        {
            if (reproducido)
            {
                reproducido = false;
            }
            genes.RePrio();

            priochange = 0;
        }
    }
    
    
    public void CheckPrio()
    {
        switch (genes.prio)
        {
            case Prio.Agua:

                objetive = AmbientManager.instance.BuscarAgua();
                BuscarComida(objetive.transform);

                if (!movement.instintos)
                {
                    movement.instintos = true;
                    BuscarComida(AmbientManager.instance.BuscarAgua().transform);
                    movement.ChangeState(Movement.States.ANYTHING);
                }


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
                
                if (!movement.instintos)
                {
                    movement.instintos = true;
                    movement.ChangeState(Movement.States.RUNNING);
                    
                }
                
                
                break;
            case Prio.Comer:
                
                if (!movement.instintos)
                {
                    GameObject temp = AmbientManager.instance.Objetives(id);
                    if (temp != null)
                    {
                        movement.instintos = true;
                        BuscarComida(temp.transform);
                        movement.ChangeState(Movement.States.FINDING);
                    }
                    else
                    {
                        Debug.LogWarning("no disponible");
                    }
                }

                break;
            case Prio.NONE: 
                //VIVIR SIN PREOCUPACION
                break;

            default:
                break;
        }

    }

    IEnumerator Curacion()
    {
        yield return new WaitForSeconds(.5f);
        
        if (genes.vida < genes.vidaMaxima )
        {
            genes.vida += genes.vidaMaxima * .1f;
            yield return new WaitForSeconds(2f);
            StartCoroutine(Curacion());
        }
        else
        {
            movement.ChangeState(Movement.States.NONE);
            movement.instintos = false;
            Debug.Log("Curacion Completa");
            StopCoroutine(Curacion());
        }

    }

    public override void Beber(float sed)
    {
        if (genes.sed < genes.sedMax)
        {
            genes.sed += sed;
            if (genes.vida < genes.vidaMaxima)
            {
                genes.vida += sed / 10;
            }

        }
        else
        {
            genes.sed = genes.sedMax;
        }
    }

    public override void Comer(float hambre)
    {
        if (genes.saciedad < genes.saciedadMax)
        {
            genes.saciedad += hambre;
            if (genes.vida < genes.vidaMaxima)
            {
                genes.vida += hambre / 10;
            }

        }
        else
        {
            genes.saciedad = genes.saciedadMax;
        }
    }

    public override void GetDamage(float damage)
    {
        genes.vida -= damage;
    }

    public override void Deshidratar(float sed)
    {
        genes.sed -= sed;
    }

    public void ChangeState(Prio newstate)
    {
        genes.prio = newstate;
    }

    public override void Hambre(float hambre)
    {
        genes.saciedad -= hambre; 
    }

    public override void BuscarComida(Transform position)//Buscar agua o Comida
    {
        objetive = position.gameObject;
        movement.AssingObjetive(position);
    }



    public override void BuscarPareja()
    {
        Pareja = AmbientManager.instance.BuscarPareja(id, 0, this);
    }

    public override void Rabiar(bool rabiar)
    {
        this.rabia = rabiar;
    }
    public void Heredar()
    {
        if (distanceRrepro && !reproducido)
        {
            AmbientManager.instance.InitRepro(this, Pareja.GetComponent<Oveja>(),id);
        }
    }
    public void Reproduccion()
    {
        objetive = Pareja;
        movement.AssingObjetive(Pareja.transform);
        movement.instintos = true;
        movement.ChangeState(Movement.States.REPRO);
    }
    public override void GenAction()
    {
        movement.Accion();
    }

    float RegladeTres(float stat, float maxstat)
    {
        return (stat * 100) / maxstat;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.CompareTag("Water"))
        {
            movement.ChangeState(Movement.States.EATKING);
            Beber(0.025f / Time.fixedDeltaTime);
        }

        if (other.transform.CompareTag("Grass"))
        {
            movement.ChangeState(Movement.States.EATKING);
            movement.instintos = true;
            Comer(0.25f / Time.fixedDeltaTime);

        }

        if (other.transform.CompareTag("Animal"))
        {
            movement.ChangeState(Movement.States.RUNNING);
            movement.instintos = true;

        }
        if (other.CompareTag("Wall"))
        {
            transform.Rotate(0, Time.deltaTime * 30, 0, Space.Self);
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
