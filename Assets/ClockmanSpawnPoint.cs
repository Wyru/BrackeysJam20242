using UnityEngine;

public class ClockmanSpawnPoint : MonoBehaviour
{
  public bool alreadyUsed = false;

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Player"))
    {
      TeleportBoss();
    }
  }

  void TeleportBoss()
  {
    if (alreadyUsed)
      return;

    alreadyUsed = true;

    var x = FindObjectOfType<ClockmanBehavior>();
    if (x == null)
    {
      Debug.LogError("Nenhum Clockman encontrado na cena!");
      return;
    }


    x.transform.position = transform.position;

  }
}
