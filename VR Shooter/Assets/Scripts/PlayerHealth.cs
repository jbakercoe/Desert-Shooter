using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DamageEvent : UnityEvent { }

public class PlayerHealth : MonoBehaviour {

    public DamageEvent OnTakeDamage = new DamageEvent();
    public DamageEvent OnPlayerDie = new DamageEvent();

    int maxHealth = 100;
    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("EnemyWeapon"))
        {
            // Take Damage
            print("I'm hit! Current health: " + currentHealth);
            TakeDamage();
        }
    }

    void TakeDamage()
    {
        currentHealth -= 5;
        if(currentHealth > 0)
        {
            OnTakeDamage.Invoke();
        }
        else
        {
            OnPlayerDie.Invoke();
        }
    }

}
