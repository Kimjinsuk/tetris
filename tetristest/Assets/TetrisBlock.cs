using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour {

    float currentMoveTime = 0f;
    public bool controlable = true;

    protected float moveSpeedTime = CommonDefine.DEFAULT_MOVE_TIME;
    //public GameManager gamemanager;

    protected float addMoveSpeedTime = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        currentMoveTime += Time.deltaTime + addMoveSpeedTime;

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

        if (GameManager.Instance.IsValidPosition(this.transform)==false)
        {
            transform.localPosition += new Vector3(-deltaMovement, 0f, 0f);
        }
    }

    //내려간다
    public void Falling()
    {
        float moveDistance = CommonDefine.BLOCK_SIZE;
        transform.localPosition += new Vector3(0f, -moveDistance, 0f);

        //바닥에 떨어졌을때
        if (GameManager.Instance.IsValidPosition(this.transform) == false)
        {
            transform.localPosition += new Vector3(0f, moveDistance, 0f);
            GameManager.Instance.OnPlaceBlock(this);
        }
    }

    //빨리내린다
    public void FallingDirectly()
    {
        controlable = false;
        moveSpeedTime = CommonDefine.FALLDIRECTLY_MOVE_TIME;
    }

    public void AddMoveSpeedTime(float addTime)
    {
        addMoveSpeedTime = addTime;
    }

    public void Rotate()
    {
        transform.Rotate(new Vector3(0f, 0f, 1f), -90.0f);
        if (GameManager.Instance.IsValidPosition(this.transform)==false)
        {
            transform.Rotate(new Vector3(0f, 0f, 1f), 90.0f);
        }
    }
}
