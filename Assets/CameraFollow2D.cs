using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    [SerializeField]
    GameObject player;
    [SerializeField]
    float timeOffset;
    [SerializeField]
    Vector2 posOffset;

    [SerializeField]
    float leftLimit;
    [SerializeField]
    float rightLimit;
    [SerializeField]
    float bottomLimit;
    [SerializeField]
    float topLimit;

    private Vector3 velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 startPos = transform.position;

        Vector3 endPos = player.transform.position;

        endPos.x += posOffset.x;
        endPos.y += posOffset.y;
        endPos.z = -10;

        transform.position = Vector3.Lerp(startPos, endPos, timeOffset * Time.deltaTime);

        transform.position = new Vector3
        (
            Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
              Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
              transform.position.z
          ) ;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //top boundary line
        Gizmos.DrawLine(new Vector2(leftLimit,topLimit),new Vector2(rightLimit,topLimit));
        //right boundary line
        Gizmos.DrawLine(new Vector2(rightLimit,topLimit), new Vector2(rightLimit, bottomLimit));
        //bottom boundary line
        Gizmos.DrawLine(new Vector2(rightLimit, bottomLimit), new Vector2(leftLimit, bottomLimit));
        //left boundary line
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(leftLimit, topLimit));
    }
}
