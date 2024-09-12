using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellDiverArrow : MonoBehaviour
{
    public SpriteRenderer theSRArrow;
    public Sprite defaultImage;
    public KeyCode keyToPress;
    public bool _rightKey;
    public bool _keyPressed;

    void Start()
    {
        theSRArrow.sprite = defaultImage;
    }

    void Update(){

    }
    public void CorrectPressed(){

    }

    public void WrongPressed()
    {

    }
}
