using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct BackgroundPiece
{
    public BACKGROUND_PIECE_TYPE type;
    public Transform piece;
}

public class UIBackgroundManager : MonoBehaviour {

    public UISprite spritePiece;


    //인스펙터에서 숨긴다
    [HideInInspector]
    public float borderLeftPosition = 0f;
    [HideInInspector]
    public float borderRightPosition = 0f;
    [HideInInspector]
    public float borderBottomPosition = 0f;

    protected BackgroundPiece[,] backgroundPieces; //가로세로 배열

    static public UIBackgroundManager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Use this for initialization
    void Start () {
        init();
	}

    public  void init()
    {
        //처음생성할때
        if (backgroundPieces == null)
        {
            backgroundPieces = new BackgroundPiece[CommonDefine.MAX_BACKGROUND_WIDTH, CommonDefine.MAX_BACKGROUND_HEIGHT];
            for (int i = 0; i < CommonDefine.MAX_BACKGROUND_WIDTH; ++i)
            {
                for (int j = 0; j < CommonDefine.MAX_BACKGROUND_HEIGHT; ++j)
                {
                    backgroundPieces[i, j].piece = CreateBackgroundPiece(i, j);
                    backgroundPieces[i, j].type = BACKGROUND_PIECE_TYPE.Blank; //초기화
                }
            }
        }
        else
        {
            ClearBackgroundPieces();
        }
    }

    //백그라운드 초기화
    public void ClearBackgroundPieces()
    {
        for (int y = 0; y < CommonDefine.MAX_BACKGROUND_HEIGHT; y++)
        {
            for (int x = 0; x < CommonDefine.MAX_BACKGROUND_WIDTH; x++)
            {
                backgroundPieces[x, y].type = BACKGROUND_PIECE_TYPE.Blank;
                backgroundPieces[x, y].piece.GetComponent<UISprite>().color = spritePiece.color;
            }
        }
    }

    public Transform CreateBackgroundPiece(int x, int y)
    {
        //background piece 생성
        GameObject piece = GameObject.Instantiate(spritePiece.gameObject);

        //부모 설정
        piece.transform.parent = this.gameObject.transform;

        piece.transform.localPosition = new Vector3(x * CommonDefine.BLOCK_SIZE, -y * CommonDefine.BLOCK_SIZE, 0);

        //NGUI의 영향으로 변한 스케일 1로 설정
        piece.transform.localScale = Vector3.one;

        //벽위치 잡기
        if (x == 0 && borderLeftPosition == 0f) //왼쪽 //설정이 안됐다면..
        {
            borderLeftPosition = piece.transform.position.x;
        }
        if (x == CommonDefine.MAX_BACKGROUND_WIDTH - 1 && borderRightPosition == 0f)    //오른쪽 // 설정이 안됐다면
        {
            borderRightPosition = piece.transform.position.x;
        }
        if (y == CommonDefine.MAX_BACKGROUND_HEIGHT - 1 && borderBottomPosition == 0f)  //바닥 //설정이 안됐다면
        {
            borderBottomPosition = piece.transform.position.y;
        }

        //피스 활성화
        piece.SetActive(true);

        return piece.transform;
    }

    //스프라이트 바꿔주기
    protected void SetBlockPiece(Transform piece, int x, int y)
    {
        //백그라운드의 피스의 스프라이트
        UISprite backSprite = backgroundPieces[x, y].piece.GetComponent<UISprite>();


        //블럭의 피스의 스프라이트
        UISprite pieceSprite = piece.GetComponent<UISprite>();


        backSprite.color = pieceSprite.color;

        backgroundPieces[x, y].type = BACKGROUND_PIECE_TYPE.Hold;

    }

    //라인삭제
    public void DeleteRows()
    {
        //밑에서 위로 - 위에거 아래로 내려야하기 때문
        for (int y = CommonDefine.MAX_BACKGROUND_HEIGHT - 1; y > 0; --y)
        {
            if (IsRowFull(y))
            {
                //현재줄 지우고
                for (int x = 0; x < CommonDefine.MAX_BACKGROUND_WIDTH; ++x)
                {
                    backgroundPieces[x, y].type = BACKGROUND_PIECE_TYPE.Blank;
                    backgroundPieces[x, y].piece.GetComponent<UISprite>().color = spritePiece.color;
                }

                //아래로 당기기
                DecreaseRow(y - 1);

                ++y;
            }
        }
    }

    //라인체킹
    bool IsRowFull(int y)
    {
        for (int x = 0; x < CommonDefine.MAX_BACKGROUND_WIDTH; ++x)
        {
            if (backgroundPieces[x, y].type == BACKGROUND_PIECE_TYPE.Blank)
                return false;
        }
        return true;
    }

    //한칸 밑으로 내리기
    void DecreaseRow(int y)
    {
        //밑에서부터 위
        for (int i = y; i > 0; --i)
        {
            for (int x = 0; x < CommonDefine.MAX_BACKGROUND_WIDTH; ++x)
            {
                //현재위치를 아래로 내리기
                backgroundPieces[x, i + 1].type = backgroundPieces[x, i].type;
                backgroundPieces[x, i + 1].piece.GetComponent<UISprite>().color = backgroundPieces[x, i].piece.GetComponent<UISprite>().color;

                //비워주기
                backgroundPieces[x, i].type = BACKGROUND_PIECE_TYPE.Blank;
                backgroundPieces[x, i].piece.GetComponent<UISprite>().color = spritePiece.color;
            }
        }
    }

    public void OnPlacePiece(Transform piece)
    {
        int x;
        int y;

        GetPiecePosition(piece, out x, out y);

        SetBlockPiece(piece, x, y);

    }

    //피스를 넣으면 XY값을 내보낸다
    public void GetPiecePosition(Transform piece, out int x, out int y)
    {
        //초기화
        x = -1;
        y = -1;

        for (int i = 0; i < CommonDefine.MAX_BACKGROUND_WIDTH; ++i)
        {
            for (int j = 0; j < CommonDefine.MAX_BACKGROUND_HEIGHT; ++j)
            {
                BoxCollider collider = backgroundPieces[i, j].piece.GetComponent<BoxCollider>();
                if (collider.bounds.Contains(piece.position) == true)
                {
                    //배경의 위치를 찾음
                    x = i;
                    y = j;
                    break;
                }
            }
        }
    }

    public bool IsExistHoldPiece(Transform piece)
    {
        int x;
        int y;

        GetPiecePosition(piece, out x, out y);

        //만들어졌을때

        if (x < 0 || y < 0) return false;

        return backgroundPieces[x, y].type == BACKGROUND_PIECE_TYPE.Hold ? true : false;

    }

    // Update is called once per frame
    void Update () {
		
	}
}
