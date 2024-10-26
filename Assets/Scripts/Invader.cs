using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[4];
    
    // Maybe make time between animations random??
    public float animationTime;

    public int invaderType;

    SpriteRenderer spRend;
    int animationFrame;
    public GameObject Blod;
    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];

    }

    void Start()
    {
        //Anropar AnimateSprite med ett visst tidsintervall
        InvokeRepeating( nameof(AnimateSprite) , animationTime, animationTime);
    }

    //pandlar mellan olika sprited för att skapa en animation
    private void AnimateSprite()
    {
        animationFrame++;
        if(animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            //spawnar ett partikel system som gör att det kommer blod.
            Instantiate(Blod, new Vector3(transform.position.x, transform.position.y, transform.position.z - 2), Quaternion.identity);


            GameManager.Instance.OnInvaderKilled(this);
            
        }
        else if(collision.gameObject.layer == LayerMask.NameToLayer("Boundary")) //nått nedre kanten
        {
            GameManager.Instance.OnBoundaryReached();
        }
    }

}
