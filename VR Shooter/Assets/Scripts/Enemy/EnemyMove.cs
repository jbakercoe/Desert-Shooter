using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ReachedTargetEvent : UnityEvent { }

public class EnemyMove : MonoBehaviour {

    public ReachedTargetEvent OnReachedTarget = new ReachedTargetEvent();

    private static float walkSpeed = 3f;
    
    [SerializeField]
    float stoppingDistance = 2.5f;
    [SerializeField]
    float recoilDistance = 5f;

    Vector3 targetLocation;
    bool isAlive = true;
    bool hasReachedStoppingDistance = false;

    void Start()
    {
        targetLocation = Vector3.zero;
    }

    void Update()
    {
        if (isAlive && !hasReachedStoppingDistance)
        {
            transform.LookAt(targetLocation);
            if (Vector3.Distance(transform.position, targetLocation) > stoppingDistance)
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else
            {
                hasReachedStoppingDistance = true;
                OnReachedTarget.Invoke();
            }
        }
    }

    // Called on die to stop enemy from moving
    public void Die()
    {
        isAlive = false;
    }

}
