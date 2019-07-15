using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesMovement : MonoBehaviour
{
    Vector3 pos;
    int dir = -1;
    [SerializeField]
    float down = -0.5f;
    [SerializeField]
    float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // left or right
        pos.x += speed * dir * Time.deltaTime;
        transform.position = pos;
        if (transform.position.x <= -1f)
        {
            dir = 1;
            pos.y += down; // go down only at the border
            transform.position = new Vector2(-1f, pos.y);
        }

        if (transform.position.x >= 1f)
        {
            dir = -1;
            pos.y += down; // go down only at the border
            transform.position = new Vector2(1f, pos.y);
        }
    }


}
