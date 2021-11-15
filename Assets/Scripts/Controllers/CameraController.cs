using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject John;
    public Vector2 MinCamPos, MaxCamPos;
    public float SmoothTime;
    public bool BossFight = false;

    private Vector2 velocity;

    // Update is called once per frame
    void Update(){

        if (John != null && !BossFight) {
            float posX = Mathf.SmoothDamp(transform.position.x,
            John.transform.position.x, ref velocity.x, SmoothTime);
            float posY = Mathf.SmoothDamp(transform.position.y,
                John.transform.position.y, ref velocity.y, SmoothTime);

            transform.position = new Vector3(
                Mathf.Clamp(posX, MinCamPos.x, MaxCamPos.x),
                Mathf.Clamp(posY, MinCamPos.y, MaxCamPos.y),
                transform.position.z);
        }
        if (BossFight)
        {
            transform.position = new Vector3(MaxCamPos.x, MinCamPos.y, transform.position.z);
        }
    }
}
