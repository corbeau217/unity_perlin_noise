using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteNoiseGenerator : ShaderManager
{
    public int seedValue;
    [Space(10)]
    public RawImage previewImage;

    public override void AssignComputeBuffers(int kernelIndex){
        computeShader.SetTexture(kernelIndex, ouputName, outputTexture);

        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);

        computeShader.SetInt("seed", seedValue);
    }
    public override void UpdatePreview(){
        previewImage.texture = this.outputTexture;
    }

}
