using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;

    public KeyCode keyToPress;
    private GameObject _onPointTrigger;
    public float _perfectDistance = 0.2f;
    public float _goodDistance = 0.4f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {        
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                float dist = Vector2.Distance(_onPointTrigger.transform.position , transform.position);
                if(dist <= _perfectDistance)
                {
                    GameManager.instance.PerfectHit();
                }else if(dist <= _goodDistance)
                {
                    GameManager.instance.GoodHit();
                }else
                {
                    GameManager.instance.NormalHit();
                }
                
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Activator")
        {
            _onPointTrigger = other.gameObject;
            canBePressed  = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (gameObject.activeSelf)
        {
            if (other.tag == "activator")
            {
                _onPointTrigger = null;
                canBePressed = false;
                GameManager.instance.NoteMissed();
                Destroy(gameObject,5);
            }
       }
    }
}
