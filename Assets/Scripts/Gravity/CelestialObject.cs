using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    public enum Size { Object, Satelite, Celestial, Planet , Star };

    [SerializeField] protected List<CelestialObject> interactables = new List<CelestialObject>();
    [SerializeField] protected float mass;
    [SerializeField] protected float surfaceGravity;
    [SerializeField] protected float radius;
    [SerializeField] protected Color color;
    [SerializeField] protected bool enableSameSizeInteract = false;
    [SerializeField] protected Size size;
    [SerializeField] protected Rigidbody2D body;
    [SerializeField] protected Vector3 initSpeed = new Vector3();
    [SerializeField] protected CelestialObject parent;
    [SerializeField] protected SpriteRenderer rend;


    // Start is called before the first frame update
    void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        radius = transform.localScale.x * 10;
        SetMass();
        body.mass = mass;
        var interacts = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject obj in interacts)
        {
            if (obj.gameObject != this.gameObject && 
                (obj.GetSize() > size || obj.GetSize() == size && enableSameSizeInteract)) interactables.Add(obj);
        }
        SetSpriteColor();
        /*GameObject gf = new GameObject("GravityField");
        gf.transform.SetParent(transform);*/
        gameObject.AddComponent<TestRadius>();
    }
    public void SetSpriteColor()
    {
        rend = GetComponent<SpriteRenderer>();
        if (rend == null) rend = GetComponentInChildren<SpriteRenderer>();

        var trail = GetComponent<TrailRenderer>();
        if (trail == null) trail = GetComponentInChildren<TrailRenderer>();

        if (color.r == color.g && color.g == color.b && color.b == 0f)
        {
            var h = Random.Range(0f, 1f);
            var s = 1f;
            var v = 0.5f;

            color = Color.HSVToRGB(h, s, v);
        }

        /*var sprite = rend.sprite;
        MaterialPropertyBlock block = new MaterialPropertyBlock();
        block.SetTexture("_MainTex", SpriteMaker.GetInstance().ColorSaturateTexture(sprite, color, FilterMode.Bilinear));
        rend.SetPropertyBlock(block);*/
        var size = rend.size;
        rend.sprite = SpriteMaker.GetInstance().ColorSaturateSprite(rend.sprite, color, FilterMode.Bilinear);
        rend.size = 100f * size;


        if (trail == null) return;
        // A simple 2 color gradient with a fixed alpha of 1.0f.
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(1f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) }
        );
        trail.colorGradient = gradient;
    }
    

    public void SetColor(Color color)
    {
        this.color = color;
    }
    public Color GetColor()
    {
        return color;
    }
    public float GetRadius()
    {
        return radius;
    }
    public void SetRadius()
    {
        radius = transform.localScale.x * 10;
    }
    public float GetMass()
    {
        return mass;
    }
    public void AddMass(float value)
    {
        mass += value;
    }
    public Rigidbody2D GetBody()
    {
        return body;
    }
    public Size GetSize()
    {
        return size;
    }
    public void SetSize(Size size)
    {
        this.size = size;
        interactables.Clear();

        var interacts = FindObjectsOfType<CelestialObject>();
        foreach (CelestialObject obj in interacts)
        {
            var i = 0f;
            Debug.Log("Check : " + i);
            if (obj.gameObject != this.gameObject &&
                (obj.GetSize() > size || obj.GetSize() == size && enableSameSizeInteract)) interactables.Add(obj);
            i++;
        }

    }
    public bool IsInteractablePresent(CelestialObject celObject)
    {
        foreach(CelestialObject obj in interactables)
        {
            if (celObject == obj) return true;
        }
        return false;
    }
    public void SetInteractables(List<CelestialObject> list)
    {
        interactables = list;
    }
    virtual public List<CelestialObject> GetInteractables()
    {
        return interactables;
    }

    public Vector3 Position
    {
        get
        {
            return body.position;
        }
    }
    virtual public void UpdateVelocity(Vector3 acceleration, float timeStep)
    {

    }

    virtual public void UpdatePosition(float timeStep)
    {

    }

    public void SetMass()
    {
        mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
    }
    public float GetGravity()
    {
        return surfaceGravity;
    }
    public void SetGravity(float value)
    {
        surfaceGravity = value;
        SetMass();
        body.mass = surfaceGravity * radius * radius / Universe.gravitationalConstant;
    }
}
