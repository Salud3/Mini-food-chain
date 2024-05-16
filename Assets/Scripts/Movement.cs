using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Movement : MonoBehaviour
{
    [Header("Variables movimiento")]

    public float velMov;
    public float velRot;
    public int movement;//accion a tomar
    public float timeMov = 2;//cada cuanto toma acciones
    float distPared = 3.5f;// Distancia minima a una pared
    public enum States { NONE = 0, WALKING = 1, HUNGRY = 2, RUNNING = 3, FINDING = 4, EATKING = 5, REPRO = 6, ANYTHING = 7 };
    //NONE ES PARA NO HACER NADA
    //WALKING ES MOVERSE RANDOM
    //HUNGRY ES TENER HAMBRE PARA MOVERSE HACIA LA COMIDA
    //FINDING ES "HUNTING" UNICAMENTE PARA DEPREDADORES NO HERVIVOROS
    //EATKING ES PARA BEBER Y COMER EN UN ESPACIO DETERMINADO
    //REPRO ES CUANDO ESTAN MOVIENDOSE HACIA SU PAREJA
    //ANYTHING ES PARA DEFINIR UN MOMENTO DE TRANQUILIDAD PERO NO ES IGUAL A NONE

    public float distance;

    public Transform objetivo; //puede ser comida o presa
    [Header("Maquina de Estados")]
    public States estado;

    public bool girar = false; 

    private void Start()
    {
        Accion();
    }
    void Update()
    {
        ColisionesAnimales();

        Movimiento();
        if (objetivo != null)
        {
            distance = Vector3.Distance(this.transform.position, objetivo.position);
        }
    }

    public void AssingObjetive( Transform transform)
    {
        objetivo = transform;
    }

    public bool distanceRrepro;

    void MovementToObjective(int cas)
    {
        var step = (velMov / 10) * Time.deltaTime;
        switch (cas)
        {
            case 0://Pareja o Comida hervivoros
                if (distance > 3.5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, step);
                }
                else
                {
                    transform.Rotate(Vector3.up * 5 * Time.deltaTime);
                    distanceRrepro = true;
                }
                break;
            case 1://Depredadores
                step = (velMov / 5) * Time.deltaTime;
                if (distance > 0.5)
                {
                    transform.position = Vector3.MoveTowards(transform.position, objetivo.transform.position, step);
                    ataccando = false;
                }
                else
                {
                    ataccando = true;
                }
            break; 
            default:
                break;
        }
    }
    public bool ataccando;

    void LookAtObjetive()
    {
        if (objetivo != null)
        {
            Vector3 direction = (objetivo.transform.position - transform.position);
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime *    velRot);
        }
        else
        {
            ChangeState((States)Random.Range(0, 2));
        }
    }
    private void Movimiento()//Se mueve, gira o no hace nada
    {
        if (girar)
        {
            transform.Rotate(Vector3.up * velRot * Time.deltaTime);
        }

        switch (estado)
        {
            //NONE ES PARA NO HACER NADA ES UN VOID ENTRE LAS DESICIONES
            case States.NONE:
                break;
            //WALKING ES MOVERSE PA'LANTE
            case States.WALKING:

                transform.position += (transform.forward * (velMov / 10) * Time.deltaTime);
                break;
            //HUNGRY ES TENER HAMBRE PARA MOVERSE HACIA LA COMIDA
            case States.HUNGRY:
                MovementToObjective(0);
                LookAtObjetive();
                break;
            case States.RUNNING:
                transform.position += (transform.forward * (velMov / 5) * Time.deltaTime);//Movimiento default con velocidad extra
                break;
            //FINDING ES "HUNTING" 
            ///UNICAMENTE PARA DEPREDADORES NO HERVIVOROS
            case States.FINDING:
                MovementToObjective(1);
                LookAtObjetive();


                break;
            //EATKING ES PARA BEBER Y COMER EN UN ESPACIO DETERMINADO
            case States.EATKING:
                transform.position += (transform.forward * (velMov / 20) * Time.deltaTime);//Movimiento default superlento

                break;
            //REPRO ES CUANDO ESTAN MOVIENDOSE HACIA SU PAREJA
            case States.REPRO:
                MovementToObjective(0);
                LookAtObjetive();
                break;
            //ANYTHING ES PARA DEFINIR UN MOVIMIENTO A UN OBJETIVO
            case States.ANYTHING:
                Debug.Log("Reflexionando acerca de la vida...");
                MovementToObjective(0);
                LookAtObjetive();
                break;
            default:
                Debug.LogWarning(estado + "Error state movement");
                break;
        }
    }

    public void ChangeState(States newstate)
    {
        estado = newstate;
    }

    private void ColisionesAnimales()//tiene una pared o animal enfrente
    {
        RaycastHit hit;
        
        if (Physics.Raycast(transform.position, transform.forward, out hit, distPared))
        {
            if (hit.collider && !hit.collider.gameObject.CompareTag("Animal") && 
                !hit.collider.gameObject.CompareTag("Water") && !hit.collider.gameObject.CompareTag("Grass") )
            {
                Debug.Log("Impacto de frente");
                girar = true;
                StartCoroutine(TiempoGirar());
            }
        }

        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distPared, Color.yellow);

    }

    public bool instintos;

    // desicion de girar, caminar y nada
    public void Accion()
    {
        if (velRot ==0 || velRot >20 || velRot <-20)
        {
            AssingRotationSpeed();
        }

        if (!instintos)
        {
            movement = Random.Range(0, 3);
        }
        else
        {
            movement = 8;
            Debug.Log("Saltando Accion: instintos Activados");
        }

        switch(movement)
        {
            case 0:
                ChangeState(States.NONE);
                break;
            case 1:
                ChangeState(States.WALKING);
                break;
            case 2:
                girar = true;
                StartCoroutine(TiempoGirar());
                break;
            default:

                break;
        }

        if (!instintos)
        {
            Invoke("Accion", timeMov);
        }
    }

    private void AssingRotationSpeed()
    {
        velRot = (Random.Range(-3.5f, 3.6f) * (velMov / 10));
        if (velRot > 20 || velRot < -20)
        {
            velRot /= 1.5f;
        }else if(velRot > -2 && velRot < 2)
        {
            velRot = 5;
            velRot *= -1;
        }
    }

    IEnumerator TiempoGirar()
    {
        yield return new WaitForSeconds(2.5f);
        girar = false;
    }

}
