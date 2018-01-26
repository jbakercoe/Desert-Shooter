using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class Player : Singleton<Player> {

    [SerializeField] Slider healthSlider;
    [SerializeField] Image red;
    [SerializeField] GameObject shotgun;
    [SerializeField] float timeBetweenShots = 1.1f;

    private int maxHealth = 100;
    private int currentHealth;
    private bool isPlaying;
    private bool hasCompletedLevel = false;
    private bool readyToFire = true;
    private Animator gunAnim;
    private AudioSource gunAudio;

    //public bool HasCompletedLevel
    //{
    //    //get { return hasCompletedLevel; }
    //    //set { hasCompletedLevel = value; }
    //}

    void Awake()
    {
        Assert.IsNotNull(healthSlider);
        Assert.IsNotNull(red);
        Assert.IsNotNull(shotgun);
    }

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        gunAnim = shotgun.GetComponent<Animator>();
        gunAudio = shotgun.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {

        if (isPlaying)
        {
            if (currentHealth <= 0)
            {
                isPlaying = false;
                GameManager.Instance.PlayerHasDied();
            } else if (Input.GetButtonDown("Fire1") && readyToFire)
            {
                gunAnim.SetTrigger("Fire");
                gunAudio.Play();
                readyToFire = false;
                StartCoroutine(WaitForNextShot());
            }
        }
        
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyAttack") && isPlaying)
        {
            takeHit();
        }
    }

    private void takeHit()
    {
        currentHealth -= 5;
        healthSlider.value = currentHealth;
        red.GetComponent<Animator>().SetTrigger("Hit");
    }

    IEnumerator WaitForNextShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToFire = true;
    }

    public void ResetHealth()
    {
        isPlaying = true;
        currentHealth = maxHealth;
        healthSlider.value = currentHealth;
    }

}
