using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class UseItemSoundBehavior : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip foodSound;
    public AudioClip drinkSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnEnable()
    {
        PlayerItemBehavior.OnUseItem += PlayItemSound;
    }
    private void OnDisable()
    {
        PlayerItemBehavior.OnUseItem -= PlayItemSound;
    }

    void PlayItemSound(ItemsController item)
    {
        if (item.isGuarana)
        {
            audioSource.PlayOneShot(drinkSound);
            return;
        }

        audioSource.PlayOneShot(foodSound);
    }
}
