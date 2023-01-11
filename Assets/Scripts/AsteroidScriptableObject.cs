using UnityEngine;

[CreateAssetMenu(fileName = "AsteroidData", menuName = "ScriptableObjects/Asteroid", order = 1)]
public class AsteroidScriptableObject : ScriptableObject
{
    public int HealthPoints; 
    
    public float LifetimeMin;
    public float LifetimeMax;
}
