using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class FireEvent : UnityEvent { }

public class FireGun : MonoBehaviour {

    public FireEvent OnFireEvent = new FireEvent();

    [SerializeField]
    float bulletSpeed;
    [SerializeField]
    Transform fireLocation;
    [SerializeField]
    GameObject fireballPrefab;
    [SerializeField]
    [Range(0, 5)]
    float recoilTime = 1.5f;

    bool isAbleToShoot = true;

    public void Shoot()
    {
        if (isAbleToShoot)
        {
            isAbleToShoot = false;
            OnFireEvent.Invoke();
            GameObject fireball = Instantiate(fireballPrefab, fireLocation.transform.position, fireLocation.transform.rotation);
            fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * bulletSpeed;
            fireball.GetComponent<ParticleSystem>().Play();
            StartCoroutine(WaitForRecoil());
            Destroy(fireball, 6f);
        }
    }

    IEnumerator WaitForRecoil()
    {
        yield return new WaitForSeconds(recoilTime);
        isAbleToShoot = true;
    }

}
