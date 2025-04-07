using UnityEngine;

public class CharController : MonoBehaviour
{
    
    Rigidbody rb;
    void Start()
    {
        
    }
    void Update(){
        if (Input.GetKeyDown(KeyCode.W))
        { 
           transform.position += new Vector3(0, 0, 1) * Time.deltaTime;
        }
        
    }
}
