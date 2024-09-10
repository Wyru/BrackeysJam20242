
using UnityEngine;

public class TVScript : MonoBehaviour
{


    public MeshRenderer m_renderer;
    [SerializeField]
    public Texture[] tex;
    [SerializeField]
    private float time = 0;
    public bool rand = true;
    // Update is called once per frame
    void Update()
    {
        if (rand)
        {
            if(time > 10f){
            m_renderer.materials[1].SetTexture("_BaseMap", tex[Random.Range(0,4)]);
                time = 0f;
            }
        }
        else
        {
            time += Time.deltaTime;
            if (time > 10f && time < 20f)
            {
                m_renderer.materials[1].SetTexture("_BaseMap", tex[0]);
            }
            else if (time > 20f && time < 30f)
            {
                m_renderer.materials[1].SetTexture("_BaseMap", tex[1]);
            }
            else if (time > 30f && time < 40f)
            {
                m_renderer.materials[1].SetTexture("_BaseMap", tex[2]);
            }
            else if (time > 40f)
            {
                m_renderer.materials[1].SetTexture("_BaseMap", tex[3]);
                time = 0f;
            }
        }

    }
}
