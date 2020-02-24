using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    Vector2 firstPressPos;
    public GameObject[] levels;
    public static int  curlevel = 0;
    public GameObject background;
    bool ispressing = false;
    private float xlevel = 0;
    public GameObject button;
    public Animator anim;
    private float height;
    public FixedJoystick variableJoystick;

    void Start()
    {
        StartCoroutine(ExampleCoroutine());
 
    }
    // Update is called once per frame
    void Update()
    {
        height = Screen.height;
        float direction = variableJoystick.Horizontal;
        if (direction == 0)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                ispressing = true;
                xlevel = Input.mousePosition.x;
                Debug.Log(Input.mousePosition.y);
            }
            if (Input.GetButtonUp("Fire1"))
                ispressing = false;
       
            if (ispressing && curlevel!=-2)
            {

                if (Input.mousePosition.y > height*3/4)// uppest FLOOR OPTIONS
                {
                    levels[curlevel + 2].transform.position += (Input.mousePosition.x - xlevel) / 10 * Vector3.right;
                    xlevel = Input.mousePosition.x;
                }

                else if (Input.mousePosition.y < height * 1 / 3)// uppest FLOOR OPTIONS
                {
                    levels[curlevel ].transform.position += (Input.mousePosition.x - xlevel) / 10 * Vector3.right;
                    xlevel = Input.mousePosition.x;
                }
            }
        }
    }
    
    IEnumerator ExampleCoroutine()
    {
        yield return new WaitForSeconds(3);

        button.SetActive(true);
    }
    IEnumerator ExampleCoroutine2()
    {
        yield return new WaitForSeconds(5);

        background.SetActive(false);
    }

    public void Startgame()
    {
        button.SetActive(false);
        anim.SetBool("Start", true);
        StartCoroutine(ExampleCoroutine2());
            }

    public void EndGame()
    {
        Application.Quit();
    }


}