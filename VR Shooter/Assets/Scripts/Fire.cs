using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour {

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] Transform fireLocation;
    [SerializeField] GameObject bulletPrefab;

    public void ShootFireBall()
    {
        GameObject fireball = Instantiate(bulletPrefab, fireLocation.transform.position, fireLocation.transform.rotation) as GameObject;
        fireball.GetComponent<Rigidbody>().velocity = fireball.transform.forward * bulletSpeed;
        fireball.GetComponent<ParticleSystem>().Play();
        Destroy(fireball, 6f);
    }

}
