using UnityEngine;
using System.Collections;

public class ParallaxScrolling : MonoBehaviour 
{
    Camera camera;
    public float ParallaxFactor;
    Vector3 previous_CameraTransform;

    void Start () {
        camera = Camera.main;
        previous_CameraTransform = camera.transform.position;
	}
	
	void Update () {
        Vector3 delta = camera.transform.position - previous_CameraTransform;
        delta.y = 0; delta.z = 0;
        transform.position += delta / ParallaxFactor;
        previous_CameraTransform = camera.transform.position;
	}


}
