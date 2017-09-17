using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIBackgroundManager : MonoBehaviour {

    public UISprite spritePiece;

	// Use this for initialization
	void Start () {
        init();
	}

    private void init()
    {
        for (int i = 0; i < CommonDefine.MAX_BACKGROUND_WIDTH; ++i)
        {
            for (int j = 0; j < CommonDefine.MAX_BACKGROUND_HEIGHT; ++j)
            {
                //background piece 생성
                GameObject piece = GameObject.Instantiate(spritePiece.gameObject);

                //부모 설정
                piece.transform.parent = this.gameObject.transform;

                piece.transform.localPosition = new Vector3(i * CommonDefine.BLOCK_SIZE, -j*CommonDefine.BLOCK_SIZE, 0);

                //NGUI의 영향으로 변한 스케일 1로 설정
                piece.transform.localScale = Vector3.one;

                piece.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
