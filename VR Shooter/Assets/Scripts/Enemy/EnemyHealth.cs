using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class KillEvent : UnityEvent { }

public class EnemyHealth : MonoBehaviour {

    // Serializable event in inspector. Good for adding listeners that are also on this GameObject
    public KillEvent OnEnemyKill = new KillEvent();
    // Static action, good for listeners on other GameObjects
    public static UnityAction OnEnemyKilled;

    CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            // kill enemy. Destroy bullet object. Invoke EnemyKilled events
            print("Bullets --- My OnLY WeaKNess");
            Destroy(other.gameObject);
            OnEnemyKilled();
            OnEnemyKill.Invoke();
        }
    }

}
