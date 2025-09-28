using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util {
    // convert to float array
    public static float[] ToFloatArray(this Vector2Int selfReference){
        float[] resultArr = new float[2];
        resultArr[0] = selfReference.x;
        resultArr[1] = selfReference.y;
        return resultArr;
    }

    // update just the translation and the scale in 2 dimensions
    //  given Vector2Int inputs
    public static Matrix4x4 UpdateTranslationScale2DInt(this Matrix4x4 selfReference, Vector2Int translation2D, Vector2Int scale2D){
        selfReference.SetTRS(
            // translation
            new Vector3(translation2D.x, translation2D.y, 0.0f),
            // rotation (dont need it)
            Quaternion.identity,
            // scale
            new Vector3(scale2D.x, scale2D.y, 1.0f)
        );
        return selfReference;
    }
}