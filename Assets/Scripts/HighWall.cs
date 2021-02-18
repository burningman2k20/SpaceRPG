using UnityEngine;

public class HighWall : MonoBehaviour
{
    private float goalAlpha;
    private Renderer render;
    private Material m_material;

    public Material OpaqueMaterial;
    public Material TranspMaterial;
    private Color _color;

    void Start()
    {
        goalAlpha = 1f;
        render = GetComponent<Renderer>();
        m_material = render.material;
    }

    void Update()
    {
        _color = render.material.color;

        if (_color.a > 0 && _color.a >= goalAlpha)
        {
            _color.a -= 0.01f;
            SetTranspMaterialColour();
        } // Approach the goal alpha value

        if (_color.a < 1f && _color.a <= goalAlpha)
        {
            _color.a += 0.01f;
            SetTranspMaterialColour();
        }
        // GetComponent<Renderer>().material.SetColor("_Color", color); // Basic opacity change, works      

        //Not sure what you are trying to do here but it will override any setting below 1
        //if (goalAlpha < 1f) // Reset the material to opaque
        //{
        //    goalAlpha = 1f;
        //    render.material = OpaqueMaterial;
        //}

    }

    //There is no point in setting the colour every frame
    private void SetTranspMaterialColour()
    {
        TranspMaterial.SetColor("_Color", _color);
    }

    public void FadeOut(float alpha)
    {
        goalAlpha = alpha;
        render.material = TranspMaterial;
        // m_material = TranspMaterial; // Won't work? Why not, since m_material should be GetComponent<Renderer>().material ...?
    }
}