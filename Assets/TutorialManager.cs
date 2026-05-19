using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
   public GameObject[] popUps;
   private int popUpIndex;
   public GameObject Obstacle;
   void Update()
    {   
        for (int i = 0; i < popUps.Length; i++) // loops control which popup is displayed
        { 
            if (i == popUpIndex)
            {
                popUps[i].SetActive(true);
            }
            else
            {
                popUps[i].SetActive(false);
            }
        }
        if (popUpIndex == 0) // tutorial started
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow)) // left and right 
            {
                popUpIndex++;
            }
            
        } else if (popUpIndex == 1) // JUMP
            {
                if (Input.GetKeyDown(KeyCode.Space))  
                {
                    popUpIndex++;
                }
            }
            else if (popUpIndex == 2) // obstacles
            {
                Obstacle.SetActive(true);
            }
    }
}

