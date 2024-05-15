using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEditor.AnimatedValues;
using UnityEditorInternal;
using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager instance;
    [Header("Animales Prefabs y Log system")]
    public List<GameObject> Prefabs = new List<GameObject>();
    public List<GameObject> pollos, ovejas, lobos, osos, aguilas, condor;
    [Header("Zonas y comida Hervivoros")]
    public GameObject[] zonas;
    public GameObject[] Pasto;
    public GameObject[] Agua;
    public int sumatory;

    private void Awake()
    {

        pollos = new List<GameObject>();
        ovejas = new List<GameObject>();
        lobos = new List<GameObject>();
        osos = new List<GameObject>();
        aguilas = new List<GameObject>();
        condor = new List<GameObject>();
        instance = this;
    }

    public void Start()
    {
        SpawnPollos(5);
        SpawnOvejas(5);
        SpawnLobos(5);
        SpawnOso(3);
        Aguilas(5);
        SpawnCondor(2);
    }
    public void SpawnPollos(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject awa;
            awa = Instantiate(Prefabs[0], zonas[0].transform.position, Quaternion.identity);
            pollos.Add(awa);

        }
    }
    public void SpawnOvejas(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject awa;
            awa = Instantiate(Prefabs[1], zonas[1].transform.position, Quaternion.identity);
            ovejas.Add(awa);

        }
    }
    public void SpawnLobos(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject awa;
            awa = Instantiate(Prefabs[2], zonas[2].transform.position, Quaternion.identity);
            lobos.Add(awa);

        }
    }
    public void SpawnOso(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject awa;
            awa = Instantiate(Prefabs[3], zonas[3].transform.position, Quaternion.identity);
            osos.Add(awa);

        }
    }
    public void Aguilas(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject Aguila;
            Aguila = Instantiate(Prefabs[4], zonas[4].transform.position, Quaternion.identity);
            aguilas.Add(Aguila);
        }
    }
    public void SpawnCondor(int cant)
    {
        for (int i = 0; i < cant; i++)
        {
            GameObject awa;
            awa = Instantiate(Prefabs[5], zonas[5].transform.position, Quaternion.identity);
            condor.Add(awa);

        }
    }
    public void Spawn()
    {
        for (int i = 0; i < 7; i++)
        {
            int cant = Random.Range(5, 8);

            for (int j = 1; j < cant+1; j++)
            {
                GameObject awa;
                awa = Instantiate(Prefabs[i], zonas[i].transform.position, Quaternion.identity);

                switch (i)
                {
                    case 0:
                        pollos.Add(awa);
                        break;
                    case 1:
                        ovejas.Add(awa);
                        break;
                    case 2:
                        lobos.Add(awa);
                        break;
                    case 3:
                        osos.Add(awa);
                        break;
                    case 4:
                        aguilas.Add(awa);
                        break;
                    case 5:
                        condor.Add(awa);
                        break;
                    case 6:
                        break;
                    default:
                        break;
                }
            }
            
        }
    }

    public int IDGenerator()
    {

        sumatory = pollos.Count + ovejas.Count + lobos.Count + osos.Count + aguilas.Count + condor.Count;
        return sumatory * Random.Range(1,16);
    }

    public GameObject BuscarAgua()
    {
        return Agua[Random.Range(0, Agua.Length)];
    }
    public GameObject BuscarPasto()
    {
        return Pasto[Random.Range(0, Pasto.Length)];
    }

    public GameObject RabiaObjective()
    {
        GameObject temp = null;
        int seed = Random.Range(0,6);
        switch (seed)
        {
            case 0:
                temp = pollos[Random.Range(0, pollos.Count - 1)];
                break;
            case 1:
                temp = ovejas[Random.Range(0, ovejas.Count - 1)];
                break;
            case 2:
                temp = lobos[Random.Range(0, lobos.Count - 1)];
                break;
            case 3:
                temp = osos[Random.Range(0, osos.Count - 1)];
                break;
            default:
                temp = pollos[Random.Range(0, pollos.Count - 1)];
                break;
        }
        return temp;
    }

    public GameObject Objetives(int clase)
    {
        GameObject temp = null;
        switch (clase)
        {
            case 0:
                temp = Pasto[Random.Range(1, Pasto.Length-1)];
                break;
            case 1:
                temp = Pasto[Random.Range(1, Pasto.Length-1)];
                break;
            case 2:
                int rand = Random.Range(0, 3);
                switch (rand)
                {
                    case 0:
                        temp = pollos[Random.Range(0, pollos.Count - 1)];
                        break;
                    case 1:
                        temp = ovejas[Random.Range(0, ovejas.Count - 1)];
                        break;
                    default:
                        temp = ovejas[Random.Range(0, ovejas.Count - 1)];
                        break;
                }
                break;
            case 3:
                int ran = Random.Range(0, 3);
                switch (ran)
                {
                    case 0:
                        ran = Random.Range(0, lobos.Count - 1);
                        temp = lobos[ran];
                        
                        break;
                    case 1:
                        ran = Random.Range(0, aguilas.Count - 1);
                        temp = aguilas[ran];
                        //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(4);
                        break;
                    case 2:
                        ran = Random.Range(0, condor.Count - 1);
                        temp = condor[ran];
                        //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(5);

                        break;
                    default:
                        break;
                }
                break;
            case 4:
                ran = Random.Range(0, osos.Count - 1);
                temp = osos[ran];
                break;
            case 5:
                ran = Random.Range(0, condor.Count - 1);
                temp = condor[ran];
                break;
            default:
                ran = Random.Range(0, pollos.Count - 1);
                temp = pollos[ran];
                break;
        }
        return temp;
    }
    
    //public List<GameObject> pollos, ovejas, lobos, osos, aguilas, condor;
    public GameObject BuscarPareja(int id, int counter, Animals buscador)
    {
        GameObject temp = null;
        if (counter > 2)
        {
            Debug.Log("No disponible");
            return null;
        }
        else
        {
            counter++;
        }
        switch (id)
        {
            case 0://POLLO
                temp = pollos[Random.Range(0, pollos.Count - 1)];

                Pollo pollo = temp.GetComponent<Pollo>();
                if (pollo.Pareja == null)//revisamos si no tiene pareja 
                {
                    pollo.Pareja = buscador.gameObject;
                }
                else//Si tiene pareja
                {
                    temp = BuscarPareja(id, counter, buscador);

                }

                break;
            case 1://OVEJA
                temp = ovejas[Random.Range(0, ovejas.Count - 1)];//buscamos la pareja
                Oveja ovj_temp = temp.GetComponent<Oveja>();
                if (ovj_temp != buscador && ovj_temp.Pareja == null)//revisamos si no tiene pareja 
                {
                    ovj_temp.Pareja = buscador.gameObject;
                }
                else//Si tiene pareja
                {
                    temp = BuscarPareja(id, counter,buscador);

                }
                break;
            case 2://LOBO
                temp = lobos[Random.Range(0, lobos.Count - 1)];
                Lobo lobo = temp.GetComponent<Lobo>();
                if (lobo.Pareja == null)
                {
                    lobo.Pareja= buscador.gameObject;
                }
                else
                {
                    temp = BuscarPareja(id , counter, buscador);
                }
                break;
            case 3://OSO
                temp = osos[Random.Range(0, osos.Count - 1)];
                Oso oso = temp.GetComponent<Oso>();
                if (oso.Pareja == null)
                {
                    oso.Pareja = buscador.gameObject;
                }
                else
                {
                    temp = BuscarPareja(id, counter, buscador);
                }
                break;
            case 4://AGUILA
                temp = aguilas[Random.Range(0, aguilas.Count - 1)];
                Aguila aguila = temp.GetComponent<Aguila>();

                if (aguila.Pareja == null)
                {
                    aguila.Pareja = buscador.gameObject;
                }
                else
                {
                    temp = BuscarPareja(id, counter,buscador);

                }
                break;
            case 5://CONDOR
                temp = condor[Random.Range(0, condor.Count - 1)];
                Condor _condor = temp.GetComponent<Condor>();
                if (_condor.Pareja == null)
                {
                    _condor.Pareja= buscador.gameObject;
                }
                else
                {
                    temp = BuscarPareja(id,counter,buscador);
                }
                break;
            default:
                temp = null;
                Debug.LogWarning("id invalida");
                break;
        }
        if (temp == null && counter>2)
        {
            Debug.LogWarning("Parejas Ocupadas");
        }
        return temp;
    }

    public void InitRepro(Animals a1, Animals a2, int id)
    {
        
        GameObject a3 = Instantiate(Prefabs[id], a1.transform.position, Quaternion.identity);
        switch (id)
        {
            case 0:
                a3.GetComponent<Pollo>().genes = a1.genes.Reproduce(a2.genes);
                pollos.Add(a3);
                a1.GetComponent<Pollo>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 1:
                a3.GetComponent<Oveja>().genes = a1.genes.Reproduce(a2.genes);
                ovejas.Add(a3);
                a1.GetComponent<Oveja>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 2:
                a3.GetComponent<Lobo>().genes = a1.genes.Reproduce(a2.genes);
                ovejas.Add(a3);
                a1.GetComponent<Lobo>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 3:
                a3.GetComponent<Oso>().genes = a1.genes.Reproduce(a2.genes);
                ovejas.Add(a3);
                a1.GetComponent<Oso>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 4:
                a3.GetComponent<Aguila>().genes = a1.genes.Reproduce(a2.genes);
                ovejas.Add(a3);
                a1.GetComponent<Aguila>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 5:
                a3.GetComponent<Condor>().genes = a1.genes.Reproduce(a2.genes);
                ovejas.Add(a3);
                a1.GetComponent<Condor>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            default:
                break;
        }

        a1.genes.RePrio();
        a1.GenAction();
        a2.genes.RePrio();
        a2.GenAction();
    }


}
