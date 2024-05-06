using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager instance;
    public List<GameObject> pollos, ovejas, lobos, osos, aguilas, condor;
    public GameObject[] Pasto;
    public GameObject Hongo;

    private void Awake()
    {
        instance = this;
    }
    public int IDGenerator()
    {
        int sumatory = pollos.Count + ovejas.Count + lobos.Count + osos.Count + aguilas.Count + condor.Count;
        return sumatory * Random.Range(0,16);
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
                game = osos[Random.Range(0, osos.Count - 1)];
                break;
            default:
                game = pollos[Random.Range(0, pollos.Count - 1)];
                break;
        }
        return game;
    }

    public GameObject Objetives(int clase)
    {
        GameObject game = null;
        switch (clase)
        {
            case 0:
                game = Pasto[Random.Range(1, Pasto.Length-1)];
                break;
            case 1:
                game = Pasto[Random.Range(1, Pasto.Length-1)];
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
                game = osos[Random.Range(0, osos.Count - 1)];
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
