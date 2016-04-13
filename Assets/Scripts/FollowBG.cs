using UnityEngine;
using System.Collections;

public class FollowBG : MonoBehaviour {



    public float parralax = 3f;
	
	void Update () {

        MeshRenderer mr = GetComponent<MeshRenderer>();
        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x = transform.position.x / transform.localScale.x / parralax;
        offset.y = transform.position.y / transform.localScale.y / parralax;

        mat.mainTextureOffset = offset;


    }
}
