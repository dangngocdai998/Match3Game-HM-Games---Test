using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class PoolingInfo
{
    public string keyName;
    public GameObject prefab;
    public int countStartSpawn;

}

public class PoolingController : MonoBehaviour
{
    [Header("GAMEOBJECT POOL")]
    public PoolingInfo[] gameObjectPoolingInfo;
    // List<GameObject> listObjectPool = new List<GameObject>();

    Dictionary<string, List<GameObject>> dicPooling = new Dictionary<string, List<GameObject>>();
    private void Start()
    {
        StartSpawnObject();
    }
    void StartSpawnObject()
    {
        foreach (PoolingInfo pooling in gameObjectPoolingInfo)
        {
            List<GameObject> go = new List<GameObject>();
            for (int i = 0; i < pooling.countStartSpawn; i++)
            {
                GameObject obj = Instantiate(pooling.prefab, transform);
                obj.name = pooling.keyName;
                obj.SetActive(false);
                go.Add(obj);
            }
            dicPooling.Add(pooling.keyName + "_Pool", go);
        }
    }
    public GameObject GetObject(string keyName)
    {
        string nameObj = keyName + "_Pool";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf && obj.name == keyName.ToString())
                {
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (keyName == gameObjectPoolingInfo[i].keyName)
                {
                    GameObject go = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    go.name = keyName.ToString();
                    dicPooling[nameObj].Add(go);
                    return go;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (keyName == gameObjectPoolingInfo[i].keyName)
                {
                    GameObject go = Instantiate(gameObjectPoolingInfo[i].prefab, transform);
                    go.name = keyName;
                    dicPooling[nameObj].Add(go);
                    return go;
                }
            }
        }

        return null;
    }
    public GameObject GetObject(string keyName, Vector3 position, Transform parent = null)
    {
        string nameObj = keyName + "_Pool";
        if (dicPooling[nameObj].Count > 0)
        {
            foreach (GameObject obj in dicPooling[nameObj])
            {
                if (!obj.activeSelf && obj.name == keyName.ToString())
                {
                    if (parent == null)
                    {
                        obj.transform.SetParent(transform);
                    }
                    else
                    {
                        obj.transform.SetParent(parent);
                    }

                    obj.transform.position = position;
                    obj.SetActive(true);
                    return obj;
                }
            }

            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (keyName == gameObjectPoolingInfo[i].keyName)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, position, Quaternion.identity, transform);
                    vfx.name = keyName.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }

                    vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }
        else
        {
            for (int i = 0; i < gameObjectPoolingInfo.Length; i++)
            {
                if (keyName == gameObjectPoolingInfo[i].keyName)
                {
                    GameObject vfx = Instantiate(gameObjectPoolingInfo[i].prefab, position, Quaternion.identity, transform);
                    vfx.name = keyName.ToString();
                    if (parent == null)
                    {
                        vfx.transform.SetParent(transform);
                    }
                    else
                    {
                        vfx.transform.SetParent(parent);
                    }

                    vfx.transform.position = position;
                    dicPooling[nameObj].Add(vfx);
                    return vfx;
                }
            }
        }

        return null;
    }
    public void DisableAllObject()
    {
        StopAllCoroutines();

        foreach (string key in dicPooling.Keys)
        {
            for (int i = dicPooling[key].Count - 1; i >= 0; i--)
            {
                if (dicPooling[key][i] == null)
                {
                    dicPooling[key].RemoveAt(i);
                    // obj.transform.SetParent(transform);
                }
                else
                {
                    dicPooling[key][i].SetActive(false);
                    dicPooling[key][i].transform.SetParent(transform);

                }
            }
        }

    }
    public void DisableAllObjectByKey(string key)
    {
        key = $"{key}_Pool";
        for (int i = dicPooling[key].Count - 1; i >= 0; i--)
        {
            if (dicPooling[key][i] == null)
            {
                dicPooling[key].RemoveAt(i);
                // obj.transform.SetParent(transform);
            }
            else
            {
                dicPooling[key][i].SetActive(false);
                dicPooling[key][i].transform.SetParent(transform);

            }
        }

    }
    public void DisableObjPooling(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(transform);
    }

}
