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
        SpawnPollos(GameManager.Instance.cantPollos);
        SpawnOvejas(GameManager.Instance.cantOveja);
        SpawnLobos(GameManager.Instance.cantLobos);
        SpawnOso(GameManager.Instance.cantOsos);
        Aguilas(GameManager.Instance.cantAguilas);
        SpawnCondor(GameManager.Instance.cantCondor);
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

    public void CheckAnimals()
    {
        aguilas = new List<GameObject>();
        Aguila[] agui = FindObjectsOfType<Aguila>();

        for (int i = 0; i < agui.Length; i++)
        {
            if (agui[i].vivo)
            {
                aguilas.Add(agui[i].gameObject);
            }
        }

        lobos = new List<GameObject>();
        Lobo[] lob = FindObjectsOfType<Lobo>();

        for (int i = 0; i < lob.Length; i++)
        {
            if (lob[i].vivo)
            {
                lobos.Add(lob[i].gameObject);
            }
        }

        osos = new List<GameObject>();
        Oso[] oso = FindObjectsOfType<Oso>();

        for (int i = 0; i < oso.Length; i++)
        {
            if (oso[i].vivo)
            {
                osos.Add(oso[i].gameObject);
            }
        }

        condor = new List<GameObject>();
        Condor[] cond = FindObjectsOfType<Condor>();

        for (int i = 0; i < cond.Length; i++)
        {
            if (cond[i].vivo)
            {
                condor.Add(cond[i].gameObject);
            }
        }

        pollos = new List<GameObject>();
        Pollo[] pol = FindObjectsOfType<Pollo>();

        for (int i = 0; i < pol.Length; i++)
        {
            if (pol[i].vivo)
            {
                pollos.Add(pol[i].gameObject);
            }
        }

        ovejas = new List<GameObject>();
        Oveja[] ov = FindObjectsOfType<Oveja>();

        for (int i = 0; i < ov.Length; i++)
        {
            if (ov[i].vivo)
            {
                ovejas.Add(ov[i].gameObject);
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
                int ran = Random.Range(0, 5);
                switch (ran)
                {
                    case 0:
                        ran = Random.Range(0, lobos.Count - 1);
                        temp = lobos[ran];
                        
                        break;
                    case 1:
                        ran = Random.Range(0, pollos.Count - 1);
                        temp = pollos[ran];
                        break;
                    case 2:
                        ran = Random.Range(0, ovejas.Count - 1);
                        temp = ovejas[ran];
                        break;
                        /*case 1:
                            ran = Random.Range(0, aguilas.Count - 1);
                            temp = aguilas[ran];
                            //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(4);
                            break;
                        case 2:
                            ran = Random.Range(0, condor.Count - 1);
                            temp = condor[ran];
                            //if (game.GetComponent<Aguila>.vulnerable == false) game = Objectives(5);
                        break;
                            */
                    default:
                        ran = Random.Range(0, lobos.Count - 1);
                        temp = lobos[ran];
                        break;
                }
                break;
            case 4:
                int ram = Random.Range(0, 5);
                switch (ram)
                {
                    case 0:
                        ran = Random.Range(0, lobos.Count - 1);
                        temp = lobos[ran];
                        break;
                    case 1:
                        ran = Random.Range(0, osos.Count - 1);
                        temp = osos[ran];
                        break;
                    default:
                        ran = Random.Range(0, osos.Count - 1);
                        temp = osos[ran];
                        break;
                }
                break;
            case 5:
                ran = Random.Range(0, 3);
                switch (ran)
                {
                    case 0:
                        ran = Random.Range(0, lobos.Count - 1);
                        temp = lobos[ran];
                        break;
                    case 1:
                        ran = Random.Range(0, osos.Count - 1);
                        temp = osos[ran];
                        break;
                    case 2:
                        ran = Random.Range(0, aguilas.Count - 1);
                        temp = aguilas[ran];
                        break;
                    default:
                        ran = Random.Range(0, condor.Count - 1);
                        temp = condor[ran];
                        break;
                }
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

                if (temp != null)
                {
                Pollo pollo = temp.GetComponent<Pollo>();
                    if (pollo.Pareja == null && pollo != buscador)//revisamos si no tiene pareja 
                    {
                        pollo.Pareja = buscador.gameObject;
                    }
                    else//Si tiene pareja
                    {
                        temp = BuscarPareja(id, counter, buscador);
                    }
                }
                else//Si tiene pareja
                {
                    temp = BuscarPareja(id, counter, buscador);

                }

                break;
            case 1://OVEJA
                temp = ovejas[Random.Range(0, ovejas.Count - 1)];//buscamos la pareja
                if (temp != null )
                {
                Oveja ovj_temp = temp.GetComponent<Oveja>();
                    if (ovj_temp != buscador && ovj_temp.Pareja == null)//revisamos si no tiene pareja 
                    {
                        ovj_temp.Pareja = buscador.gameObject;
                    }
                    else//Si tiene pareja
                    {
                        temp = BuscarPareja(id, counter, buscador);

                    }
                }
                else//Si tiene pareja
                {
                    temp = BuscarPareja(id, counter,buscador);

                }
                break;
            case 2://LOBO
                temp = lobos[Random.Range(0, lobos.Count - 1)];
                if (temp != null)
                {
                Lobo lobo = temp.GetComponent<Lobo>();
                    if (lobo != buscador && lobo.Pareja == null)
                    {
                        lobo.Pareja = buscador.gameObject;
                    }
                    else//Si tiene pareja
                    {
                        temp = BuscarPareja(id, counter, buscador);

                    }
                }
                else
                {
                    temp = BuscarPareja(id , counter, buscador);
                }
                break;
            case 3://OSO
                temp = osos[Random.Range(0, osos.Count - 1)];
                if (temp != null)
                {
                Oso oso = temp.GetComponent<Oso>();
                    if (oso.Pareja == null && oso != buscador)
                    {
                        oso.Pareja = buscador.gameObject;
                    }
                    else
                    {
                        temp = BuscarPareja(id, counter, buscador);
                    }
                }
                else
                {
                    temp = BuscarPareja(id, counter, buscador);
                }
                break;
            case 4://AGUILA
                temp = aguilas[Random.Range(0, aguilas.Count - 1)];

                if (temp != null)
                {
                Aguila aguila = temp.GetComponent<Aguila>();
                    if (aguila.Pareja == null && aguila != buscador)
                    {
                        aguila.Pareja = buscador.gameObject;
                    }
                    else
                    {
                        temp = BuscarPareja(id, counter, buscador);
                    }
                }
                else
                {
                    temp = BuscarPareja(id, counter, buscador);
                }
                break;
            case 5://CONDOR
                temp = condor[Random.Range(0, condor.Count - 1)];
                if (temp != null)
                {
                Condor _condor = temp.GetComponent<Condor>();
                    if (_condor.Pareja == null && _condor != buscador)
                    {
                        _condor.Pareja = buscador.gameObject;
                    }
                    else
                    {
                        temp = BuscarPareja(id, counter, buscador);
                    }
                }
                else
                {
                    temp = BuscarPareja(id, counter, buscador);
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
                lobos.Add(a3);
                a1.GetComponent<Lobo>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 3:
                a3.GetComponent<Oso>().genes = a1.genes.Reproduce(a2.genes);
                osos.Add(a3);
                a1.GetComponent<Oso>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 4:
                a3.GetComponent<Aguila>().genes = a1.genes.Reproduce(a2.genes);
                aguilas.Add(a3);
                a1.GetComponent<Aguila>().reproducido = true;
                a1.transform.position += new Vector3(3, 0, 3);
                break;
            case 5:
                a3.GetComponent<Condor>().genes = a1.genes.Reproduce(a2.genes);
                condor.Add(a3);
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
