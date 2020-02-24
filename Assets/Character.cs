using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    [System.Serializable]
    public class levels
    {
        public GameObject curleftfloor;
        public GameObject currightfloor;
    }
    public levels[] levellist;
    private bool StairUp = false;
    private bool StairDown = false;
    private bool climbing = false;
    private int temp;
    public Sprite motion;
    private Animator anim;
    private int collected = 0;
    public  GameObject finishUi;
    public GameObject teleportloc;
    public FixedJoystick variableJoystick;

    public GameObject[] uiElements;
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    void FixedUpdate()
    {
        this.GetComponent<SpriteRenderer>().sprite = motion;
        float direction = variableJoystick.Horizontal;

        foreach (var item in levellist)
        {

            if (this.transform.position.x < item.curleftfloor.transform.position.x) //SOL
            {
                GameObject temp = item.currightfloor;
                item.currightfloor = item.curleftfloor;
                item.curleftfloor = temp;

                item.curleftfloor.transform.position += 60 * Vector3.left;
            }
            if (this.transform.position.x > item.currightfloor.transform.position.x) //sag
            {
                GameObject temp = item.curleftfloor;
                item.curleftfloor = item.currightfloor;
                item.currightfloor = temp;
            item.currightfloor.transform.position += 60 * Vector3.right;
            }
        }
        Debug.Log("dir"+direction);
        if (direction < 0 && !climbing)
        {
            this.transform.position += Vector3.left * 12 * -direction * Time.deltaTime;
            anim.SetInteger("Motion", 0);
        }
        if (direction > 0 && !climbing)
        {
            this.transform.position += Vector3.right * 12* direction * Time.deltaTime;
            anim.SetInteger("Motion", 1);

        }

        if (Input.GetKey(KeyCode.LeftArrow) && !climbing)
        {
            this.transform.position += Vector3.left * 12  * Time.deltaTime;
            anim.SetInteger("Motion", 0);
        }

        if (Input.GetKey(KeyCode.RightArrow) && !climbing)
        {
            this.transform.position += Vector3.right * 12 * Time.deltaTime;
            anim.SetInteger("Motion", 1);

        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (StairUp )
            {
                int curlvltemp = LevelManager.curlevel;
                anim.SetInteger("Motion", 2);

                LevelManager.curlevel = -2;
                StartCoroutine(MoveToPosition(1, curlvltemp+1));
                climbing = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (StairDown && !climbing)
            {
                int curlvltemp = LevelManager.curlevel;
                LevelManager.curlevel = -2;
                anim.SetInteger("Motion", 2);

                StartCoroutine(MoveToPosition(-1, curlvltemp-1));
                climbing = true;
            }
        }
    }
    public void ButtonStair()
    {
        Debug.Log("Stairup"+StairUp);

        if (StairUp)
        {
            int curlvltemp = LevelManager.curlevel;
            anim.SetInteger("Motion", 2);

            LevelManager.curlevel = -2;
            StartCoroutine(MoveToPosition(1, curlvltemp + 1));
            climbing = true;
        }
        if (StairDown && !climbing)
        {
            int curlvltemp = LevelManager.curlevel;
            LevelManager.curlevel = -2;
            anim.SetInteger("Motion", 2);

            StartCoroutine(MoveToPosition(-1, curlvltemp - 1));
            climbing = true;
        }
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.gameObject.name == "Stair")
            {
                StairUp = true;
            }
            else if (collision.gameObject.name == "StairDown")
            {
                StairDown = true;
            }
        
        else if (collision.gameObject.name == "sırınga")
        {
            Changecolor(0);
            Destroy(collision.gameObject);
            chechisFinished();
        }
        else if (collision.gameObject.name == "govde")
        {
            Changecolor(1);
            Destroy(collision.gameObject);
            chechisFinished();

        }
        else if (collision.gameObject.name == "kafa")
        {
            Changecolor(2);
            Destroy(collision.gameObject);
            chechisFinished();

        }
        else if (collision.gameObject.name == "ilac")
        {
            Changecolor(3);
            Destroy(collision.gameObject);
            chechisFinished();

        }
        else if (collision.gameObject.name == "kurabiye")
        {
            Changecolor(4);
            Destroy(collision.gameObject);
            chechisFinished();

        }
        else if (collision.gameObject.tag == "enemy")
        {
            this.transform.position = new Vector3(teleportloc.transform.position.x -3,1.62f);
            LevelManager.curlevel = 0;
        }
    }

    private void chechisFinished()
    {
        collected += 1;

        if (collected == 5)
        {
            Time.timeScale = 0f;
            finishUi.SetActive(true);
        }
    }

    public void Changecolor(int number)
    {
        uiElements[number].GetComponent<Image>().color = Color.white;
    }

    void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.name == "Stair")
            {
                StairUp = false;
            }
            else if (collision.gameObject.name == "StairDown")
            {
                StairDown = false;
            }
        }

        public IEnumerator MoveToPosition(int isUp, int curlvl)
    {
        var currentPos = this.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / 1;

            transform.position = Vector3.Lerp(currentPos, new Vector3(currentPos.x, (currentPos.y + isUp * 3.5f)), t);
            climbing = false;
            yield return null;
        }
        LevelManager.curlevel = curlvl;
        anim.SetInteger("Motion", 3);




    }
}
