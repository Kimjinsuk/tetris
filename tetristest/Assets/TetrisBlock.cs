using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour {

    float currentMoveTime = 0f;

    protected float moveSpeedTime = 0.6f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentMoveTime += Time.deltaTime;

        if (currentMoveTime >= moveSpeedTime)
        {
            //내려오자
            Falling();

            currentMoveTime = 0f;
        }
	}

    public void MoveHorizontal(Vector2 direction)
    {
        float moveDistance = CommonDefine.BLOCK_SIZE;
        float deltaMovement = (direction.Equals(Vector2.right)) ? moveDistance : -moveDistance;

        transform.localPosition += new Vector3(deltaMovement, 0f, 0f);
    }

    public void Falling()
    {
        float moveDistance = CommonDefine.BLOCK_SIZE;
        transform.localPosition += new Vector3(0f, -moveDistance, 0f);
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), -90.0f);
    }
}
