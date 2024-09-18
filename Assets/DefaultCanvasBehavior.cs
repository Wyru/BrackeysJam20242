using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DefaultCanvasBehavior : MonoBehaviour
{

    public static DefaultCanvasBehavior instance;

    public RawImage hand;
    public TextMeshProUGUI itemHoldDescription;
    public TextMeshProUGUI playerMoneyText;
    public TMP_Text possibleKeys;
    public TMP_Text bagText;
    public TMP_Text _taskText;
    public GameObject menu;
    public GameObject cartTotalObject;
    public TextMeshProUGUI itemNotifications;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Multiplas instâncias do canvas default!");
            DestroyImmediate(gameObject);
            return;
        }

        GameManager.OnMoneyChange += GameManagerOnMoneyChange;
        instance = this;
        DontDestroyOnLoad(this);
    }

    void GameManagerOnMoneyChange(int value, int moneyTotal, int moneyToday)
    {
        playerMoneyText.text = "$" + moneyTotal.ToString();
    }

    void OnDestroy()
    {
        GameManager.OnMoneyChange -= GameManagerOnMoneyChange;
    }

}
