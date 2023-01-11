using UnityEngine;
using Random = UnityEngine.Random;

public class Asteroid : View
{
    private bool m_destroyed;
    public bool Destroyed
    {
        get => m_destroyed; 
        set => SetValue(value, ref m_destroyed);
    }
    
    private int m_healthPoints;
    public int HealthPoints
    {
        get => m_healthPoints; 
        set => SetValue(value, ref m_healthPoints);
    }

    public AsteroidScriptableObject AsteroidScriptableObject;

    private Renderer m_renderer;

    private float m_lifetime;
    private float m_lifetimeCounter;

    private MouseClicks m_mouseClicks = new MouseClicks();

    private void Start()
    {
        SetInitialAsteroidValues();
        m_renderer = GetComponent<Renderer>();
    }
    
    private void OnEnable() => SetInitialAsteroidValues();

    private void SetInitialAsteroidValues()
    {
        m_lifetime = Random.Range(AsteroidScriptableObject.LifetimeMin, AsteroidScriptableObject.LifetimeMax);
        HealthPoints = AsteroidScriptableObject.HealthPoints;
    }

    private void Update()
    {
        m_lifetimeCounter += Time.deltaTime;
        
        if (m_lifetimeCounter >= m_lifetime)
            DeactivateAsteroid();
    }
    
    private void OnMouseOver()
    {
        if (!Input.GetMouseButtonDown(0)) 
            return;

        if (!m_mouseClicks.DoubleMouseClicked) 
            return;
        
        HealthPoints--;

        if (HealthPoints == 0)
        {
            Destroyed = true;
            DeactivateAsteroid();
            return;
        }

        SetColor();
    }
    
    private void SetColor() => m_renderer.material.color = AsteroidHelper.SetColor(HealthPoints);
    

    private void  DeactivateAsteroid() => gameObject.SetActive(false);
}