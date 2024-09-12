using UnityEngine;


public class RandomOpenedGates : MonoBehaviour
{

    //default will always be open. Not all doors will be tirggered.
    public GameObject open;
    public GameObject closed;

    // Start is called before the first frame update
    [System.Obsolete]
    void Awake()
    {
        if (Random.Range(0, 100) > 49)
        {
            open.SetActive(true);
            closed.SetActive(false);
        }
        else
        {
            open.SetActive(false);
            closed.SetActive(true);
        }
    }
}
