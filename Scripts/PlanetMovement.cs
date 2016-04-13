using UnityEngine;
using System.Collections;

//  Class is responsible for circural moving of planets
public class PlanetMovement : MonoBehaviour {

    private float angle = 0;
    public float speed;
    private float _speed;          //2*PI in degress is 360, so you get 5 seconds to complete a circle
    public float radius = 5;
    private float x;
    private float y;

    private Vector3 _position;

    private bool firstLoop = true;
    private float randNumber;

    public bool rotate = true;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        _speed = (2 * Mathf.PI) / speed;

        // Checks if the scene is just starded and randomize the position of the plantes.
        // In other cases all planets would start from the same y axis position.
        if (firstLoop)
        {
            angle = Random.Range(0, 100);
            firstLoop = false;
        }

        angle += _speed * Time.deltaTime;

        x = transform.parent.position.x + Mathf.Cos(angle) * radius;
        y = transform.parent.position.y + Mathf.Sin(angle) * radius ;

        _position = new Vector3(x , y , 0f);
        transform.position = _position;

        if (rotate)
        {
            RotatePlanet();
        }
    }

    private void RotatePlanet()
    {
        transform.Rotate(0,0,Time.deltaTime*20);
    }
}