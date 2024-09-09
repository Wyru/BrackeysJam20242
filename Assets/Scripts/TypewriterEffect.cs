using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
  public float typingSpeed = 0.05f;
  public AudioClip typingSound;
  public TextMeshProUGUI textMeshProUGUI;
  public AudioSource audioSource;
  private Coroutine typingCoroutine;
  bool isSkipping = false;
  public bool isTyping = false;

  void Start()
  {

  }

  public void StartTyping(string text)
  {
    if (typingCoroutine != null)
    {
      StopCoroutine(typingCoroutine);
    }
    isTyping = true;
    typingCoroutine = StartCoroutine(TypeText(text));
  }

  private IEnumerator TypeText(string text)
  {
    textMeshProUGUI.text = "";

    for (int i = 0; i < text.Length; i++)
    {
      if (isSkipping)
      {
        textMeshProUGUI.text = text;
        isSkipping = true;
        break;
      }

      textMeshProUGUI.text += text[i];

      if (typingSound != null)
      {
        audioSource.PlayOneShot(typingSound);
      }

      yield return new WaitForSeconds(typingSpeed);
    }

    if (!isSkipping)
    {
      textMeshProUGUI.text = text;
    }

    isSkipping = false;
    isTyping = false;
  }

  public void Skip()
  {
    isSkipping = true;
  }
}
