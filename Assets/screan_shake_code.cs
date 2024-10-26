using UnityEditor.Tilemaps;
using UnityEngine;

public class screan_shake_code : MonoBehaviour
{
    // Start is called before the first frame update
    public float shake;
    
    void Start()
    {
        //shake = 5;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // gör att cameran åker melan två punkter snappt bllir närmre varandra och det bllir screan shake
        transform.position = new Vector3(Random.Range(-shake, shake), Random.Range(-shake, shake), -10);
        if (shake > 0f) shake -= Time.deltaTime * 10f;
        shake = Mathf.Clamp(shake, 0, 100);
    }
}
