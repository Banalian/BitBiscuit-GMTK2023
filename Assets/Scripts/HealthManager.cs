using UnityEngine;
using UnityEngine.Events;

public class HealthManager : MonoBehaviour
{

    /// <summary>
    /// Maximum health of the brick.
    /// </summary>
    [field: SerializeField]
    public int MaxHealth { get; protected set; } = 1;
    
    /// <summary>
    /// Current health of the brick.
    /// </summary>
    [field:SerializeField]
    public int Health { get; protected set; } = 1;
    
    /// <summary>
    /// On damage taken event.
    /// </summary>
    public event UnityAction<GameObject, int> OnDamageTakenEvent;
    
    /// <summary>
    /// On death event.
    /// </summary>
    public event UnityAction<GameObject> OnDeathEvent;
    
    private void Start()
    {
        Reset();
    }

    /// <summary>
    /// Method to damage the object.
    /// </summary>
    /// <param name="damage">Amount of damage to inflict</param>
    public void Damage(int damage)
    {
        Health -= damage;
        OnDamageTakenEvent?.Invoke(gameObject, damage);
        if (Health <= 0)
        {
            Health = 0;
            OnDeathEvent?.Invoke(gameObject);
        }
    }
    
    /// <summary>
    /// Method to instantly kill the object.
    /// </summary>
    public void Kill()
    {
        Health = 0;
        OnDeathEvent?.Invoke(gameObject);
    }

    /// <summary>
    /// Resets the component
    /// </summary>
    public void Reset()
    {
        Health = MaxHealth;
    }

    public void SetParameters(int newMaxHealth, bool resetHealth = true)
    {
        MaxHealth = newMaxHealth;
        if (resetHealth)
        {
            Reset();
        }
    }
}