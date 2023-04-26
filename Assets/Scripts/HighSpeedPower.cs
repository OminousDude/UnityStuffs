using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighSpeedPower : MonoBehaviour
{
    private float highSpeed = 16f;
    private float highSpeedLength = 10f;

    [SerializeField] private Rigidbody2D rbPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(speedUp());
        }
    }

    private IEnumerator speedUp()
    {
        Player1.speed = highSpeed;
        Debug.Log("hi1");
        //gameObject.(false);
        yield return new WaitForSeconds(highSpeedLength);
        Debug.Log("hi2");
        Player1.ResetSpeed();
        //Destroy(gameObject, 0f);
    }
}
