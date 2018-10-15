using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDisposer : MonoBehaviour {

    /// <summary>
    /// Determines what to do once an enemy is killed
    /// </summary>

    [SerializeField]
    [Range(0f, 5f)]
    float recoilDistance = 2f;
    [SerializeField]
    [Range(0, 75)]
    float timeOfRecoil = 25;
    [SerializeField]
    [Range(0f, 5f)]
    float recoilSpeed = 1f;

    public void KnockEnemyBack()
    {
        StartCoroutine(GetKnockedBack());
    }

    IEnumerator GetKnockedBack()
    {
        float y = transform.position.y;
        int i = 0;
        while(i < 25)
        {
            transform.Translate(new Vector3(-Vector3.forward.x, 0f, -Vector3.forward.z) * recoilDistance * recoilSpeed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
            i++;
            yield return null;
        }
        Destroy(gameObject, 5f);
    }

}
