using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootballKick : MonoBehaviour
{

    public Rigidbody rb;
    private void OnCollisionEnter(Collision other) {
        if(other.gameObject.tag == "Player"){
            rb.AddForce((other.transform.forward+Vector3.up)*5, ForceMode.Impulse);
        }
    }
}
