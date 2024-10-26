using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MysteryShip : MonoBehaviour
{
    float speed = 5f;
    float cycleTime = 5f;
    public int msLives = 3;
    Vector2 leftDestination;
    Vector2 rightDestination;
    int direction = -1;
    bool isVisible;

    
    void Start()
    {
        Vector3 leftEdge = Camera.main.ViewportToWorldPoint(Vector3.zero);
        Vector3 rightEdge = Camera.main.ViewportToWorldPoint(Vector3.right);

        //positionen d�r den kommer stanna utanf�r sk�rmen.
        leftDestination = new Vector2(leftEdge.x - 2f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 2f, transform.position.y);

        SetInvisible();
    }


    void Update()
    {
        if (!isVisible) //�r den inte synlig s� ska den ej r�ra sig.
        {
            return;
        }

        if(direction == 1)
        {
            //r�r sig �t h�ger.
            transform.position += speed * Time.deltaTime * Vector3.right;

            if (transform.position.x >= rightDestination.x)
            {
                SetInvisible();
            }
        }
        else
        {
            //r�r sig �t v�nster
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x <= leftDestination.x)
            {
                SetInvisible();
            }
        }
    }

  
    //flyttar den till en plast precis utanf�r scenen.
    void SetInvisible()
    {
        isVisible = false;

        if(direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        Invoke(nameof(SetVisible), cycleTime); //anropar SetVisible efter ett visst antal sekunder.
    }

    void SetVisible()
    {
        direction *= -1; //�ndrar riktningen.

        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Liv funktion f�r MysteryShip.
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            if(msLives == 3)
            {
                msLives--;
            }
            if(msLives == 2)
            {
                msLives--;
            }
            if(msLives == 1)
            {
                SetInvisible();
                GameManager.Instance.OnMysteryShipKilled(this);
            }
            
        }
    }
}
