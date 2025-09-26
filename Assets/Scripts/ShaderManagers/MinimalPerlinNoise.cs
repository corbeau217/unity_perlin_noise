using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimalPerlinNoise : ShaderManager
{
    [Space(20)]
    public RawImage previewImage;
    [Space(10)]
    public Vector2Int gridCellCount = Vector2Int.one * 5;

    public override void AssignComputeBuffers(int kernelIndex){
        computeShader.SetTexture(kernelIndex, ouputName, outputTexture);

        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);

        computeShader.SetInt("cellCountX", gridCellCount.x);
        computeShader.SetInt("cellCountY", gridCellCount.y);
    }
    public override void UpdatePreview(){
        previewImage.texture = this.outputTexture;
    }

}
