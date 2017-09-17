using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITetrisBlockManager : MonoBehaviour {

    public GameObject[] tetrisblocks;

    protected TetrisBlock currentBlock = null;

    static public UITetrisBlockManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
	}

    public void Init()
    {
        for (int i = 0; i < this.transform.childCount; ++i)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    //랜덤으로 블록 스폰
    public void SpawnRandomBlock()
    {
        Debug.Log("Spawn!");
        int type = Random.Range(0, (int)BLOCK_TYPE.MAX);
        currentBlock = CreateBlock((BLOCK_TYPE)type, CommonDefine.MAX_BACKGROUND_WIDTH / 2, CommonDefine.BLOCK_START_Y_POSITION);
    }

    TetrisBlock CreateBlock(BLOCK_TYPE type, int x,int y)
    {
        //블록생성
        GameObject block = GameObject.Instantiate(tetrisblocks[(int)type]);

        //부모설정
        block.transform.parent = this.gameObject.transform;

        //포지션 변경
        block.transform.localPosition = new Vector3(x * CommonDefine.BLOCK_SIZE, -y * CommonDefine.BLOCK_SIZE, 0f);

        //스케일 초기화
        block.transform.localScale = Vector3.one;

        block.SetActive(true);

        return block.GetComponent<TetrisBlock>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBlock != null)
        {
            if (currentBlock.controlable == true)
            {
                if (Input.GetKeyDown(KeyCode.UpArrow) == true)
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

                //빠르게
                if (Input.GetKeyDown(KeyCode.DownArrow) == true)
                {
                    currentBlock.AddMoveSpeedTime(CommonDefine.DOWN_ADD_SPEED_TIME);
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow) == true)
                {
                    currentBlock.AddMoveSpeedTime(0f);
                }
            }

            if (Input.GetKeyDown(KeyCode.Space) == true)
            {
                currentBlock.FallingDirectly();
            }

        }
    }
}
