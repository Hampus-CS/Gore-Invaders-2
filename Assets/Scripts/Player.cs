using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public Laser laserPrefab;
    Laser laser;
    public GameObject LaserLjud;
    public float recoil = 0f;
    public GameObject fakeplayer;


    float speed = 5f;


    SpriteRenderer spRend;
    public Sprite[] playerLifeSprites = new Sprite[3];

    public GameObject PlayerHitSound;
    public GameObject PlayerDeathSound;

    private void Awake()
    {
        spRend = fakeplayer.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        Vector3 position = transform.position;

        float horizontalInput = Input.GetAxis("Horizontal");
        position.x += horizontalInput * speed * Time.deltaTime;

        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        transform.position = position;

        // flytar en fake bild av playern så det ser ut som recoil
        fakeplayer.transform.position = new Vector3(position.x, position.y-recoil, position.z);
        if (recoil > 0) recoil -= Time.deltaTime * 10f;
        recoil = Mathf.Clamp(recoil , 0 ,12.5f) ;

        if (Input.GetButtonDown("Fire1") && laser == null)
        {
            laser = Instantiate(laserPrefab, transform.position, Quaternion.identity);

            //spawnar ett objekt med ljud.
            Instantiate(LaserLjud, transform.position, Quaternion.identity);

            recoil = 0.5f;

        }
    }
    private void FixedUpdate()
    {
        //Script för spelarens utseende beroende av liv.
            if (GameManager.Instance.lives == 3)
            {
                Instantiate(PlayerHitSound, transform.position, Quaternion.identity);
                spRend.sprite = playerLifeSprites[0];
            }
            else if (GameManager.Instance.lives == 2)
            {
                Instantiate(PlayerHitSound, transform.position, Quaternion.identity);
                spRend.sprite = playerLifeSprites[1];
            }
            else if (GameManager.Instance.lives == 1)
            {
                Instantiate(PlayerHitSound, transform.position, Quaternion.identity);
                spRend.sprite = playerLifeSprites[2];
            }
            else if (GameManager.Instance.lives == 0)
            {
                Instantiate(PlayerDeathSound, transform.position, Quaternion.identity);
            }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.Health();
            Console.WriteLine("död");

        }
    }
}
