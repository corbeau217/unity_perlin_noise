using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinearPerlinNoise : ShaderManager
{
    [Space(20)]
    public ShaderManager inputNoiseManager;
    public RawImage previewImage;
    [Space(10)]
    public Vector2Int gridCellCount = Vector2Int.one * 5;
    [Space(10)]
    public RenderTexture inputNoiseTexture;
    public Vector2Int inputDimensions;

    public override void SettingOverrides(){
        safeToPerform = false;
    }
    public override void AssignComputeBuffers(int kernelIndex){
        computeShader.SetTexture(kernelIndex, ouputName, outputTexture);
        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);
        
        computeShader.SetTexture(kernelIndex, "InputNoiseImage", inputNoiseTexture);
        computeShader.SetInt("noiseTextureWidth", inputDimensions.x);
        computeShader.SetInt("noiseTextureHeight", inputDimensions.y);

        computeShader.SetInt("cellCountX", gridCellCount.x);
        computeShader.SetInt("cellCountY", gridCellCount.y);
    }
    public override void UpdatePreview(){
        previewImage.texture = this.outputTexture;
    }
    public override void TestReadyToPerform(){
        if ( (inputNoiseManager!=null) && (inputNoiseManager.HasPerformedCompute()) ){
            // grab the things
            inputNoiseTexture = inputNoiseManager.GetResultTexture();
            inputDimensions = inputNoiseManager.GetOutputDimensions();
            // mark as safe
            safeToPerform = true;
        }
        
    }

}
