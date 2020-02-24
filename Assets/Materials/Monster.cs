using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public bool goingleft = true;
    public bool inleft = false;
    public GameObject floor;
    public int speed = 5;

    void FixedUpdate()
    {
        if (goingleft)
            this.transform.position += Vector3.left * speed * Time.deltaTime;
        else
            this.transform.position += Vector3.left * -speed * Time.deltaTime;

        //if (goingleft)
        //{
        //    if (this.transform.position.x < floor.transform.position.x - 20)
        //    {
        //        goingleft = false;
        //        this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 9000f);
        //    }
        //}
        //else
        //{
        //    if (this.transform.position.x > floor.transform.position.x + 10)
        //    {
        //        this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 9000f);
        //        goingleft = true;
        //    }
        //}

    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "esc")
        {
            if (goingleft)
            {
                goingleft = false;
                this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 9000f);
            }
            else
            {
                this.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 9000f);
                goingleft = true;
            }

        }
    }
}
