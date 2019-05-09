using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    private List<GameObject> list;
    private GameObject obj; // 풀에 오브젝트가 없을 경우를 대비

    
    public void InitPool(GameObject _obj, int poolSize)
    {
        list = new List<GameObject>();

        for (int i=0; i<poolSize; i++)
        {
            obj = Instantiate(_obj);
            obj.name += i;            
            obj.SetActive(false);

            list.Add(obj);
        }
    }

    public void ShowObject(int posX, int posY)
    {
        obj = list.Find(item => item.activeSelf == false);

        //if (obj == null)
        //    return;

        obj.transform.position = new Vector3(posX, posY, 0f);
        obj.SetActive(true);
    }


}
