using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LowGravPower : MonoBehaviour
{
    private float lowGravSpeed = 1f;
    private float lowGravLength = 15f;

    [SerializeField] private Rigidbody2D rbPlayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(lowGrav());
            //Destroy(gameObject, 0f);
        }
    }

    private IEnumerator lowGrav()
    {
        float originalGravityScale = rbPlayer.gravityScale;
        rbPlayer.gravityScale = lowGravSpeed;
        yield return new WaitForSeconds(lowGravLength);
        rbPlayer.gravityScale = originalGravityScale;
    }
}
