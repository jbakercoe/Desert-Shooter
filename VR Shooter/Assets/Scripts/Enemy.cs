using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Enemy : MonoBehaviour {

    private static float walkSpeed = 3f;

    [SerializeField] Transform playerLocation;
    [SerializeField] float stoppingDistance = 2.5f;
    [SerializeField] float recoilDistance = 5f;
    [SerializeField] float recoilSpeed = 1f;
    [SerializeField] SphereCollider rightHand;
    [SerializeField] SphereCollider leftHand;

    private Animator anim;
    private Vector3 targetDir;
    private Vector3 newDir;
    private bool hasReachedAttackRange;
    private bool isAlive;
    private CapsuleCollider capsuleCollider;
    private Rigidbody rigidBody;
    private float dissapearSpeed = 2f;
    private bool isReadyToDissapear;
    private float timeSinceLastAttack;
    private bool inGameOverState = false;
    private AudioSource audioSource;

    public static float WalkSpeed
    {
        get { return walkSpeed; }
        set { walkSpeed = value; }
    }

    void Awake()
    {
        Assert.IsNotNull(playerLocation);
        Assert.IsNotNull(rightHand);
        Assert.IsNotNull(leftHand);
    }

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        isAlive = true;
        isReadyToDissapear = false;
        //inRange = false;
        hasReachedAttackRange = false;
        targetDir = new Vector3(playerLocation.position.x, 0f, playerLocation.position.z);
        leftHand.enabled = false;
        rightHand.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {

        timeSinceLastAttack += Time.deltaTime;

        if (isAlive && !GameManager.Instance.GameOver)
        {
            transform.LookAt(targetDir);
            if (Vector3.Distance(transform.position, playerLocation.position) > stoppingDistance)
            {
                transform.Translate(Vector3.forward * walkSpeed * Time.deltaTime);
            }
            else
            {
                if (!hasReachedAttackRange)
                {
                    hasReachedAttackRange = true;
                    Attack();
                }
            }
        } else if(GameManager.Instance.PlayerIsDead && !inGameOverState && isAlive)
        {
            inGameOverState = true;
            anim.SetTrigger("GameOver");
            StartCoroutine(PlayAudioLoop());
        }

        if (isReadyToDissapear)
        {
            transform.Translate(-Vector3.up * dissapearSpeed * Time.deltaTime);
        }
    }

    public void Attack()
    {
        if(!GameManager.Instance.GameOver && isAlive)
        {
            anim.SetTrigger("Attack");
            audioSource.Play();
        }

    }

    public void EnableRightHand()
    {
        // enables right hand to attack
        rightHand.enabled = true;
    }

    public void EnableLeftHand()
    {
        leftHand.enabled = true;
    }

    public void DisableRightHand()
    {
        rightHand.enabled = false;
    }

    public void DisableLeftHand()
    {
        leftHand.enabled = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        { // kill enemy
            Destroy(other.gameObject);
            isAlive = false;
            float y = transform.position.y;
            for (int i = 0; i < 25; i++)
            {
                transform.Translate(new Vector3(-Vector3.forward.x, 0f, -Vector3.forward.z) * recoilDistance * recoilSpeed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, y, transform.position.z);
            }
            anim.SetTrigger("Hit");
            GameManager.Instance.KillEnemy();
            capsuleCollider.enabled = false;
            rigidBody.isKinematic = true;
            StartCoroutine(RemoveEnemy());
        }
    }

    IEnumerator RemoveEnemy()
    {
        // Removes enemy from map, from GameManager List, then deletes object
        yield return new WaitForSeconds(3f);
        isReadyToDissapear = true;
        yield return new WaitForSeconds(2f);
        GameManager.Instance.RemoveEnemyFromList(this.gameObject);
        Destroy(gameObject);
    }

    IEnumerator PlayAudioLoop()
    {
        // Plays war cry at random intervals
        // for after gameplay
        yield return new WaitForSeconds(Random.Range(5f, 12f));
        audioSource.Play();
        StartCoroutine(PlayAudioLoop());
    }

}
