using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WhiteNoiseGenerator : MonoBehaviour
{
    public ComputeShader computeShader;
    public Vector3Int blockSize = new Vector3Int(16,16,1);
    public string computeShaderKernel = "WhiteNoise1";
    [Space(10)]
    public RenderTexture outputTexture;
    public Vector2Int outputDimensions = Vector2Int.one * 256;
    [Space(10)]
    public int seedValue;
    [Space(10)]
    public RawImage previewImage;
    
    void Start(){
        this.GenerateRenderTexture();
        this.PerformComputeShader();
    }
    void Update(){
        previewImage.texture = this.outputTexture;
    }
    void OnDestroy(){
        outputTexture?.Release();
        outputTexture = null;
    }

    private void GenerateRenderTexture(){
        this.outputTexture = new RenderTexture(outputDimensions.x, outputDimensions.y, 0);
        this.outputTexture.enableRandomWrite = true;
        this.outputTexture.Create();
    }

    public void PerformComputeShader(){
        int kernelIndex = computeShader.FindKernel(computeShaderKernel);
        
        computeShader.SetTexture(kernelIndex, "ResultImage", outputTexture);
        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);
        computeShader.SetInt("seed", seedValue);

        computeShader.Dispatch(kernelIndex, outputDimensions.x / blockSize.x, outputDimensions.y / blockSize.y, 1);
    }

}
