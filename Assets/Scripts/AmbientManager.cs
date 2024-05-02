using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager instance;
    public List<GameObject> pollos, ovejas, lobos, oso, aguilas, condor;
    public GameObject[] Pasto;
    public GameObject Hongo;

    private void Awake()
    {
        instance = this;
    }
    public Animals GenerarStats(int clase)
    {
        float sed = Random.Range(1, 10) * clase;
        float fuerza = Random.Range(0,5) * clase;
        float velocidad = Random.Range(1, 5) * clase;
        float saciedad = Random.Range(1, 10) * clase;
        float vidaMaxima = Random.Range(15, 50) * clase;
        Animals.Prio prio;
        int x = Random.Range(0, 5);

        switch (x)
        {
            case 0:
                prio = Animals.Prio.Agua;
                break;
            case 1:
                prio = Animals.Prio.Repro;
                break;
            case 2:
                prio = Animals.Prio.Huir;
                break;
            case 3:
                prio = Animals.Prio.Comer;
                break;
            default: 
                prio = Animals.Prio.Agua;
                break;
        }
        Animals animal = new Animals(sed,fuerza,velocidad,saciedad,vidaMaxima,prio);


        return animal;
    }

    public GameObject RabiaObjective()
    {
        GameObject game = null;
        int seed = Random.Range(0,6);
        switch (seed)
        {
            case 0:
                game = pollos[Random.Range(0, pollos.Count - 1)];
                break;
            case 1:
                game = ovejas[Random.Range(0, ovejas.Count - 1)];
                break;
            case 2:
                game = lobos[Random.Range(0, lobos.Count - 1)];
                break;
            case 3:
                game = oso[Random.Range(0, oso.Count - 1)];
                break;
            default:
                game = pollos[Random.Range(0, pollos.Count - 1)];
                break;
        }
        return game;
    }

    public GameObject Emparejar(int clase)
    {
        GameObject pareja = null;
        switch (clase)
        {
            case 0:
                break;
            case 1:
                pareja = ovejas[Random.Range(0, ovejas.Count - 1)];
                if (pareja.GetComponent<Oveja>().Stats.disp == false)
                {
                    pareja = Emparejar(clase);
                }
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                pareja = null;
                break;
        }

        return pareja;
    }

    public GameObject Objetives(int clase)
    {
        GameObject game = null;
        switch (clase)
        {
            case 0:
                game = Pasto[Random.Range(1, Pasto.Length)];
                break;
            case 1:
                game = Pasto[Random.Range(1, Pasto.Length)];
                break;
            case 2:
                int rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        game = ovejas[Random.Range(0, ovejas.Count - 1)];
                        break;
                    case 1:
                        game = pollos[Random.Range(0, pollos.Count - 1)];
                        break;
                    default:
                        game = ovejas[Random.Range(0, ovejas.Count - 1)];
                        break;
                }
                break;
            case 3:
                int ran = Random.Range(0, 3);
                switch (ran)
                {
                    case 0:
                        ran = Random.Range(0, lobos.Count - 1);
                        game = lobos[ran];
                        
                        break;
                    case 1:
                        ran = Random.Range(0, aguilas.Count - 1);
                        game = aguilas[ran];
                        //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(4);
                        break;
                    case 2:
                        ran = Random.Range(0, condor.Count - 1);
                        game = condor[Random.Range(0, condor.Count - 1)];
                        //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(5);

                        break;
                    default:
                        break;
                }
                break;
            case 4:
                game = oso[Random.Range(0, oso.Count - 1)];
                break;
            case 5:
                game = condor[Random.Range(0, condor.Count - 1)];
                break;
            default:
                game = pollos[Random.Range(0, pollos.Count - 1)];
                break;
        }
        return game;
    }
}
