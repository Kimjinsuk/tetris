using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BLOCK_TYPE
{
    I, //0
    T, //1
    O,
    J,
    L,
    Z,
    S,
    MAX //7(사이즈)
}

public enum STAGE_TYPE
{
    Intro,
    Main,
    MAX
}

public enum BACKGROUND_PIECE_TYPE
{
    Blank,
    Hold,
    MAX
}


public class CommonDefine : MonoBehaviour {

    public static int MAX_BACKGROUND_WIDTH = 10;  //맵 넓이
    public static int MAX_BACKGROUND_HEIGHT = 16; //맵 높이
    public static float BLOCK_SIZE = 40f;           //블록사이즈

    public static float DEFAULT_MOVE_TIME = 0.6f;
    public static float FALLDIRECTLY_MOVE_TIME = 0.0001f;

    public static float DOWN_ADD_SPEED_TIME = 0.3f;


    public static int BLOCK_START_Y_POSITION = -2;
}
