using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{

    private void Update()
    {
        StartCoroutine(bulletLife());
    }
    IEnumerator bulletLife()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Temas etti");
            Destroy(gameObject);
        }
    }
}
