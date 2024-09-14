using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour
{
  public LayerMask layerMask;

  public float radius = 1;

  public bool debugLine = true;

  public IEnumerable<Collider> GetObjectsInsideHitbox()
  {
    // Obtém todos os objetos que estão dentro do BoxCollider
    return Physics.OverlapSphere(transform.position, radius, layerMask);
  }

  void OnDrawGizmos()
  {
    Gizmos.color = Color.magenta;
    Gizmos.DrawWireSphere(transform.position, radius);
  }
}
