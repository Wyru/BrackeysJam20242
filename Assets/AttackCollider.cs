using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
  BoxCollider boxCollider;
  void Awake()
  {
    boxCollider = GetComponent<BoxCollider>();
  }

  public IEnumerable<Collider> GetObjectsInsideHitbox()
  {
    //Pega o centro do BoxCollider
    Vector3 boxCenter = boxCollider.transform.position + boxCollider.center;

    // Pega o tamanho do BoxCollider (não esqueça de ajustar para o escalonamento do objeto)
    Vector3 boxSize = boxCollider.size;
    boxSize.x *= boxCollider.transform.localScale.x;
    boxSize.y *= boxCollider.transform.localScale.y;
    boxSize.z *= boxCollider.transform.localScale.z;

    // Obtém todos os objetos que estão dentro do BoxCollider
    return Physics.OverlapBox(boxCenter, boxSize / 2, boxCollider.transform.rotation);
  }
}
