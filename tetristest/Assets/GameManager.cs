using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public GameObject[] Scenes;
    public UILabel labelStart;
    //public UIBackgroundManager uiBackground;

    static public GameManager Instance = null;

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
	
	// Update is called once per frame
	void Update () {
		
	}

    void ChangeStage(STAGE_TYPE type)
    {
        //다끄자
        for (int i =0; i < (int)STAGE_TYPE.MAX; ++i)
        {
            Scenes[i].SetActive(false);
        }

        //열릴때 필요한거 넣기
        switch (type)
        {
            case STAGE_TYPE.Intro:
                {
                    labelStart.gameObject.SetActive(false);
                    StopAllCoroutines();
                    Scenes[(int)STAGE_TYPE.Intro].SetActive(true);
                    break;
                }
            case STAGE_TYPE.Main:
                {
                    //모든 스테이지 정보 초기화
                    Scenes[(int)STAGE_TYPE.Main].SetActive(true);
                    UITetrisBlockManager.Instance.Init();
                    UIBackgroundManager.Instance.init();
                    StartCoroutine(StartGame());
                    break;
                }
        }
    }

    public void OnClickIntro()
    {
        ChangeStage(STAGE_TYPE.Main);
    }
    public void OnClickBack()
    {
        ChangeStage(STAGE_TYPE.Intro);
    }

    IEnumerator StartGame()
    {
        labelStart.gameObject.SetActive(true);

        labelStart.text = "3";
        yield return new WaitForSeconds(1f);

        labelStart.text = "2";
        yield return new WaitForSeconds(1f);

        labelStart.text = "1";
        yield return new WaitForSeconds(1f);


        labelStart.text = "Start!";
        yield return new WaitForSeconds(1f);

        labelStart.gameObject.SetActive(false);

        //블록이 내려오기 시작
        UITetrisBlockManager.Instance.SpawnRandomBlock();

    }

    //테트리스 블럭 벽 충돌 체크
    public bool IsValidPosition(Transform block)
    {
        for (int i = 0; i < block.transform.childCount; ++i)
        {
            //테트리스 블럭의 자식을 가져와서 그중하나라도 벽을 침범하는지 충돌체크를 한다
            Transform piece = block.transform.GetChild(i);

            //회전시 정확한 값으로 나오지 않을 수 있기 때문에 float이 0일때를 고려하여(왼쪽 벽에 부딪히는 경우) 값을 확실히해준다
            float x = Mathf.Round(piece.transform.position.x * 100f) / 100f;

            //벽체크
            if (x > UIBackgroundManager.Instance.borderRightPosition //오른쪽벽보다 x값이 클때
                || UIBackgroundManager.Instance.borderLeftPosition > x  //x값이 왼쪽벽보다 작을때
                || UIBackgroundManager.Instance.borderBottomPosition > piece.transform.position.y) //y값이 바닥보다 작을때
            {
                Debug.Log("crack!");
                return false;   //충돌 리턴
            }

            //블럭이 이미 존재하는지 체크
            if (UIBackgroundManager.Instance.IsExistHoldPiece(piece) == true)
                return false;
        }
        return true;
    }

    public void OnPlaceBlock(TetrisBlock block)
    {
        block.enabled = false;

        for (int i = 0; i < block.transform.childCount; ++i)
        {
            Transform piece = block.transform.GetChild(i);
            UIBackgroundManager.Instance.OnPlacePiece(piece);
        }

        //현재 블럭 죽이기
        Destroy(block.transform.gameObject);


        //줄 체크 후 삭제
        UIBackgroundManager.Instance.DeleteRows();


        UITetrisBlockManager.Instance.SpawnRandomBlock();
        //blockManager.SpawnRandomBlock();
    }
}
