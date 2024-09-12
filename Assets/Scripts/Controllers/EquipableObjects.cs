using UnityEngine;
public class EquipableObjects : MonoBehaviour, IInteractable
{
    [SerializeField]
    public int attackDamage;

    public void Interact()
    {
        PlayerController.instance.EquipWeapon(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Actor>().TakeDamage(attackDamage);
            PlayerController.instance.PlaySound();
        }
    }

}
