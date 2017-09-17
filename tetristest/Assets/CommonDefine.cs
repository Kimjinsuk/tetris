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

public class CommonDefine : MonoBehaviour {

    public static float MAX_BACKGROUND_WIDTH = 10;
    public static float MAX_BACKGROUND_HEIGHT = 16;

    public static float BLOCK_SIZE = 40f;
}
