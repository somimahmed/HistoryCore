using UnityEngine;

public class GlobeController : MonoBehaviour
{
    
    // Sensitivity
    public float rotationSpeed = 0.1f;

   void Update()
    {
        // Rotate the globe around its Y-axis at a speed of 10 degrees per second
        transform.Rotate(Vector3.down, 5 * Time.deltaTime);
        

         
     if(Input.touchCount > 0)
     {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved)
        {
           
            float rotationX = touch.deltaPosition.y * rotationSpeed;
            float rotationY = touch.deltaPosition.x * rotationSpeed;
            transform.Rotate(Vector3.up, -rotationY, Space.World);
            transform.Rotate(Vector3.right, rotationX, Space.World);
        }
     }
     // for desktop user, we can use mouse drag to rotate the globe.
     if(Input.GetMouseButton(0))
     {
        
        float rotationX = Input.GetAxis("Mouse Y") * rotationSpeed;
        float rotationY = Input.GetAxis("Mouse X") * rotationSpeed;
        transform.Rotate(Vector3.up, -rotationY, Space.World);
        transform.Rotate(Vector3.right, rotationX, Space.World);
     }
      // Zoom in and out
        if (Input.touchCount == 2){
            if(Camera.main.fieldOfView<115 && Camera.main.fieldOfView>40){
                
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
        
            Camera.main.fieldOfView += difference * 0.1f;
            }
            else{
                Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;
        
            Camera.main.fieldOfView += difference * 0.01f;
            }
           
        }
        // for desktop user, we can use mouse scroll to zoom in and out.
        if (Input.GetAxis("Mouse ScrollWheel") != 0f) {
            if(Camera.main.fieldOfView<=115 && Camera.main.fieldOfView>=40){
                 Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 10f;
            }
            else{
                Camera.main.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * 1f;
            }
           
        }
    }
   
   
    
}
