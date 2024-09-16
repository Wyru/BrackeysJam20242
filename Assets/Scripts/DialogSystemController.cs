using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DialogSystemController : MonoBehaviour
{
  public GameObject panel;
  public GameObject point;
  public static DialogSystemController Instance { get; private set; }
  public TypewriterEffect typewriterEffect;

  public bool showExample = false;

  Stack<string> quotes;

  public bool isDialogRunning = false;

  bool firstLine = false;

  Action currentOnEndCallback;

  public bool sleepingzzzz = false;

  private void Awake()
  {
    if (Instance != null)
    {
      Debug.LogWarning("Multiplos dialog system na cena!");
      Destroy(this);
      return;
    }

    Instance = this;
  }

  void Start()
  {
    if (showExample)
      ShowDialogs(new List<string>(new string[]{
        "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. ",
        "enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. ",
        "ut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. Neque porro quisquam est, qui dolorem ipsum quia dolor sit amet, consec",
      }));
  }


  void Update()
  {
    if (!isDialogRunning)
      return;

    var input = Input.GetKeyDown(KeyCode.E);


    if (quotes != null && quotes.Count > 0)
    {
      if (firstLine || input)
      {
        if (!typewriterEffect.isTyping)
        {
          typewriterEffect.StartTyping(quotes.Pop());
          firstLine = false;
          return;
        }
      }

      if (input)
        typewriterEffect.Skip();
      return;
    }
    point.SetActive(!typewriterEffect.isTyping);

    if (typewriterEffect.isTyping && input)
    {
      typewriterEffect.Skip();
      return;
    }


    if (input && quotes != null && quotes.Count == 0 && !typewriterEffect.isTyping)
    {
      isDialogRunning = false;
      panel.SetActive(false);
      currentOnEndCallback?.Invoke();
      currentOnEndCallback = null;
      typewriterEffect.textMeshProUGUI.text = "";
      sleepingzzzz = true;
      Invoke(nameof(ResetSleeping), .5f);
    }

  }

  void ResetSleeping()
  {
    sleepingzzzz = false;
  }

  public static void ShowDialogs(List<string> quotes, Action OnEndDialog = null)
  {
    if (Instance.sleepingzzzz)
      return;

    Instance.isDialogRunning = true;
    Instance.firstLine = true;
    Instance.quotes = new Stack<string>(quotes.ToArray().Reverse());
    Instance.panel.SetActive(true);
    Instance.currentOnEndCallback = OnEndDialog;
  }

}
