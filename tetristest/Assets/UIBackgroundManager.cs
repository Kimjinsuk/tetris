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
        for (int i = 0; i < 10; ++i)
        {
            for (int j = 0; j < 16; ++j)
            {
                //background piece 생성
                GameObject piece = GameObject.Instantiate(spritePiece.gameObject);

                //부모 설정
                piece.transform.parent = this.gameObject.transform;

                piece.transform.localPosition = new Vector3(i * 40, -j*40, 0);

                //NGUI의 영향으로 변한 스케일 1로 설정
                piece.transform.localScale = Vector3.one;
            }
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
