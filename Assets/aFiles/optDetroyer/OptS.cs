using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OptS : MonoBehaviour
{
    static public List<List<GameObject>> everythingLists;
    static public List<Type> types;
    static public List<GameObject> bullets;
    private void Awake()
    {
        everythingLists = new List<List<GameObject>>();
        //=========================================================== “ут заполн€ютс€ списки и типы,
        //они должны быть в равном количестве!!!!!є;%є;%"є;%:є"
        types = new List<Type> { typeof(BulletS) };
        InitLists(bullets);
        //=========================================================== “ут заполн€ютс€ списки и типы,
    }
    static public void OptDestroy<T> //вместо удалени€ выключает объект и если он предназначен дл€ сохранени€, то сохран€ет
        (GameObject currentGameObject) where T : MonoBehaviour
    {
        currentGameObject.SetActive(false);
        for (int i = 0; i < everythingLists.Count; i++)
        {
            if (typeof(T) == types[i])
            {
                everythingLists[i].Add(currentGameObject);
            }
        }
    }
    static public GameObject OptInstantiate<T> //вместо обычного создани€ экземлп€ра находит сохранЄнный выключенный и подготавливает его
        (GameObject instantiate, Vector3 objectPosition) where T : MonoBehaviour, OptDestroyable
    {
        GameObject result = null;
        for (int i = 0; i < everythingLists.Count; i++)
        {
            if (typeof(T) == types[i])
            {
                bool done = false;
                while (everythingLists[i].Count > 0 && !done)
                {
                    if (everythingLists[i][0].activeSelf == true)
                    {
                        everythingLists[i].RemoveAt(0);
                    }
                    else
                    {
                        done = true;
                        GameObject tempObject = everythingLists[i][0];
                        everythingLists[i].RemoveAt(0);
                        tempObject.transform.position = objectPosition;
                        tempObject.GetComponent<T>().ResetObject();
                        tempObject.SetActive(true);
                        result = tempObject;
                    }
                }
            }
        }
        if (result == null)
        {
            result = Instantiate(instantiate, objectPosition, Quaternion.identity);
        }
        return result;
    }
    //static void AddPrevious //заполн€ет начальными значени€ми
    //    <T>() where T : MonoBehaviour
    //{
    //    T[] array = FindObjectsOfType<T>();
    //    for (int a = 0; a < array.Length; a++)
    //    {
    //        GameObject b = array[a].gameObject;
    //    }
    //}
    void InitLists(params List<GameObject>[] p)
    {
        for (int i = 0; i < p.Length; i++)
        {
            p[i] = new List<GameObject>();
            everythingLists.Add(p[i]);
        }
    }
}
public interface OptDestroyable
{
    public void ResetObject();
}


