using UnityEngine;

public class RandomPicture : MonoBehaviour
{
    MeshRenderer m_renderer;
    [SerializeField]
    public Texture[] tex;
    [SerializeField]

    private void Awake()
    {
        m_renderer = gameObject.GetComponent<MeshRenderer>();
        m_renderer.material.SetTexture("_BaseMap", tex[Random.Range(0, tex.Length)]);
    }
}
