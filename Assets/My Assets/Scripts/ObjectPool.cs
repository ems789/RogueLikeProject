using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public List<GameObject> list = new List<GameObject>();

    public void InitPool(GameObject _obj, int poolSize)
    {   
        for (int i=0; i<poolSize; i++)
        {
            GameObject obj = Instantiate(_obj);
            obj.name += i;            
            obj.SetActive(false);

            list.Add(obj);
        }
    }

    public void GetObject(float posX, float posY)
    {
        GameObject obj = list.Find(item => item.activeSelf == false);
        
        if (obj == null)
            return;

        obj.transform.position = new Vector3(posX, posY, 0f);
        obj.SetActive(true);
    }

    public GameObject PeekObject()
    {
        GameObject obj = list.Find(item => item.activeSelf == false);
        return obj;
    }


}
