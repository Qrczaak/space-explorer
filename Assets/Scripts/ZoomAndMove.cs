using UnityEngine;
using System.Collections;

public class ZoomAndMove : TouchManager
{

    public float moveSensitivityX = 1.0f;
    public float moveSensitivityY = 1.0f;
    public bool updateZoomSensitivity = true;
    public float orthoZoomSpeed = 0.05f;
    public float minZoom = 1.0f;
    public float maxZoom = 10.0f;
    public bool invertMoveX = false;
    public bool invertMoveY = false;

    private Camera _camera;

    public GUITexture buttonTexture = null;
    public Collider2D myCollider = null;

    private float tempSpeed = 4f;

    // Use this for initialization
    void Start()
    {
        _camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput(buttonTexture);
        // _camera.transform.position = ;
        _camera.transform.Translate(new Vector3(Input.GetAxis("Horizontal") * tempSpeed, Input.GetAxis("Vertical") * tempSpeed, 0)*Time.deltaTime);
    }

    void ScreenMoved()
    {
        //_camera.backgroundColor = Color.magenta;

        if (updateZoomSensitivity)
        {
            moveSensitivityX = _camera.orthographicSize / 5.0f;
            moveSensitivityY = _camera.orthographicSize / 5.0f;
        }

        Vector2 delta = Input.touches[0].deltaPosition;

        float positionX = delta.x * moveSensitivityX * Time.deltaTime;
        positionX = invertMoveX ? positionX : positionX * -1;

        float positionY = delta.y * moveSensitivityY * Time.deltaTime;
        positionY = invertMoveY ? positionY : positionY * -1;

        _camera.transform.position += new Vector3(positionX, positionY, 0);

    }

    void ZoomInOut()
    {

        if (updateZoomSensitivity)
        {
            moveSensitivityX = _camera.orthographicSize / 5.0f;
            moveSensitivityY = _camera.orthographicSize / 5.0f;
        }

        Touch touchOne = Input.touches[0];
        Touch touchTwo = Input.touches[1];

        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
        Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

        float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
        float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;

        float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;

        _camera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, minZoom, maxZoom);
    }
}
