using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HellDiverPlayerArrows : MonoBehaviour
{
    public SpriteRenderer theSRDiver;
    public Sprite defaultImage;
    public Sprite pressedImage;

    public KeyCode keyToPress;

    void Start()
    {
        theSRDiver = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(!SequencyToPress.instance.timerError.Timeout){
            theSRDiver.sprite = defaultImage;
            return;
        }
        else 
        {
            if(Input.GetKeyDown(keyToPress))
            {
                theSRDiver.sprite = pressedImage;
                SequencyToPress.instance.CheckPlayerInput(gameObject);
            }

            if(Input.GetKeyUp(keyToPress))
            {
                theSRDiver.sprite = defaultImage;
            }
        }
        
    }

}
