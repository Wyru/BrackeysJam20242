using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    private GameObject _onPointTrigger;
    public float _perfectDistance = 0.1f;
    public float _goodDistance = 0.2f;

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
                    ArrowGameManager.instance.PerfectHit();
                }else if(dist <= _goodDistance)
                {
                    ArrowGameManager.instance.GoodHit();
                }else
                {
                    ArrowGameManager.instance.NormalHit();
                }
                
                gameObject.SetActive(false);
                Destroy(gameObject,2);
            }
        }
        transform.position -= new Vector3(0f, BeatScroller.instance.beatTempo * Time.deltaTime, 0f);
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
            if (other.tag == "Activator")
            {
                _onPointTrigger = null;
                canBePressed = false;
                ArrowGameManager.instance.NoteMissed();
                Destroy(gameObject);
            }
       }
    }
}
