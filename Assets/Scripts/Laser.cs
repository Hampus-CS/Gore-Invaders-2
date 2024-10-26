using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Laser : Projectile
{
    public GameObject LaserhitSound;
    private void Awake()
    {
        direction = Vector3.up;
    }

    void Update()
    {
        transform.position += speed * Time.deltaTime * direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckCollision(collision);
    }

    void CheckCollision(Collider2D collision)
    {
        Bunker bunker = collision.gameObject.GetComponent<Bunker>();

        if(bunker == null) //Om det inte är en bunker vi träffat så ska skottet försvinna.
        {
            //spawnar ett tomt object som gör att lasern låter
            Instantiate(LaserhitSound, transform.position, Quaternion.identity);

            //sickar en singnal att cameran skakar
            GameObject.Find("Main Camera").GetComponent<screan_shake_code>().shake = 2f;


            Destroy(gameObject);
        }
    }
}
