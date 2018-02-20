using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveLook : MonoBehaviour {

    Vector2 mouseLook;
    Vector2 smoothV;

    public float sensivity = 2.0f;
    public float smoothing = 2.0f;

    public float minimumY = -60F;
    public float maximumY = 60F;

    GameObject character;
    // Use this for initialization
    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        md = Vector2.Scale(md, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        mouseLook.y = Mathf.Clamp(mouseLook.y, minimumY, maximumY);

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);

        //transform.position = Vector3.MoveTowards(transform.position, parent.transform.position, 0.03f); Vu TPS
    }
}
