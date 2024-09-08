using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderFPSController : MonoBehaviour
{

  public float speed = 10f;
  public float rotationSpeed = 90f;  // Velocidade de rotação em graus por segundo

  void Update()
  {
    var input = Vector3.zero;

    if (Input.GetKey(KeyCode.W))
      input = transform.forward;
    if (Input.GetKey(KeyCode.S))
      input = transform.forward * -1;
    if (Input.GetKeyDown(KeyCode.D))
      RotateCharacter(90);
    if (Input.GetKeyDown(KeyCode.A))
      RotateCharacter(-90);

    transform.position += speed * Time.deltaTime * input;

  }

  void RotateCharacter(float angle)
  {
    // Rotaciona o personagem em torno do eixo Y
    transform.Rotate(Vector3.up, angle);
  }
}
