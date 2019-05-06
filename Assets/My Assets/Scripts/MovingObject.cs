using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public float speed;

    private Vector3 vector;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            vector.Set(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"), transform.position.z);

            if(vector.x != 0 || vector.y != 0)
            {
                transform.Translate(vector.x * speed * Time.deltaTime, 0, 0);
                transform.Translate(0, vector.y * speed * Time.deltaTime, 0);
            }
            //else if(vector.y != 0)
            //{
            //    transform.Translate(0, vector.y * speed * Time.deltaTime, 0);
            //}
        }
    }
}
