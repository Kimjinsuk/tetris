using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITetrisBlockManager : MonoBehaviour {

    public GameObject[] tetrisblocks;

    protected TetrisBlock currentBlock = null;

	// Use this for initialization
	void Start () {
        int type = Random.Range(0, (int)BLOCK_TYPE.MAX);
        currentBlock = CreateBlock((BLOCK_TYPE)type, 0, 0);
        //Debug.Log(currentblock.ToString());
	}
	


    TetrisBlock CreateBlock(BLOCK_TYPE type, int x,int y)
    {
        //블록생성
        GameObject block = GameObject.Instantiate(tetrisblocks[(int)type]);

        //부모설정
        block.transform.parent = this.gameObject.transform;

        //포지션 변경
        block.transform.localPosition = new Vector3(x*CommonDefine.MAX_BACKGROUND_WIDTH, y*CommonDefine.MAX_BACKGROUND_HEIGHT, 0f);

        //스케일 초기화
        block.transform.localScale = Vector3.one;

        block.SetActive(true);

        Debug.Log("!!!!!!!!!!"+block.GetComponent<TetrisBlock>());
        return block.GetComponent<TetrisBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow)==true)
            {
                currentBlock.Rotate();
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
            {
                currentBlock.MoveHorizontal(Vector2.left);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) == true)
            {
                currentBlock.MoveHorizontal(Vector2.right);
            }
        }
    }
}
