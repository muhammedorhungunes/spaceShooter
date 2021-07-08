using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public Joystick joystick; 
    public Button fireButton;

    public GameObject shot;
    public Transform[] shotSpawns;
    public float fireRate;



    private float nextFire;

    void Start()
	{
		Button btn = fireButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);

	}
     void TaskOnClick(){
	if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
	    foreach (var shotSpawn in shotSpawns){
	    	Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
	    }
	    GetComponent<AudioSource>().Play ();
        }
  }	

   

    void FixedUpdate ()
    {
        float moveHorizontal = joystick.Horizontal;
        float moveVertical = joystick.Vertical;

        Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
        GetComponent<Rigidbody>().velocity = movement * speed;

        GetComponent<Rigidbody>().position = new Vector3 
        (
            Mathf.Clamp (GetComponent<Rigidbody>().position.x, boundary.xMin, boundary.xMax), 
            0.0f, 
            Mathf.Clamp (GetComponent<Rigidbody>().position.z, boundary.zMin, boundary.zMax)
        );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }
}
