using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] GameObject[] popUps;
    int popUpIndex = 0;

    void Start()
    {
        
    }

    void Update()
    {
        /*
        for (int i = 0; i <popUps.Length; i ++)
        {
            if (i == popUpIndex)
            {
                popUps[popUpIndex].SetActive(true);
            }
            else
            {
                popUps[popUpIndex].SetActive(false);

            }
        }*/

        if (popUpIndex == 0)
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex ++;

            }
        }
        else if (popUpIndex == 1)
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUps[popUpIndex].SetActive(false);
                popUpIndex ++;
            }
        }
        else if (popUpIndex == 2)
        {
            popUps[popUpIndex].SetActive(true);
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {                
                popUps[popUpIndex].SetActive(false);
                popUpIndex ++;
            }
        }



        
    }
}
