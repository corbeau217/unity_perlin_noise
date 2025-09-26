using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderManager : MonoBehaviour
{
    public ComputeShader computeShader;
    public Vector3Int blockSize = new Vector3Int(16,16,1);
    public string computeShaderKernel = "WhiteNoise1";
    [Space(10)]
    public RenderTexture outputTexture;
    public Vector2Int outputDimensions = Vector2Int.one * 256;
    public string ouputName = "ResultImage";
    [Space(10)]
    public bool performedCompute = false;
    public bool showingOnPreview = false;
    
    void Start(){
        this.PrepareRenderTextures();
    }
    void Update(){
        if(!performedCompute){
            this.PerformComputeShader();
        }
        if(showingOnPreview){
            this.UpdatePreview();
        }
    }
    void OnDestroy(){
        outputTexture?.Release();
        outputTexture = null;
    }

    private void PrepareRenderTextures(){
        this.outputTexture = new RenderTexture(outputDimensions.x, outputDimensions.y, 0);
        this.outputTexture.enableRandomWrite = true;
        this.outputTexture.Create();
    }

    public void PerformComputeShader(){
        int kernelIndex = computeShader.FindKernel(computeShaderKernel);

        AssignComputeBuffers(kernelIndex);

        computeShader.Dispatch(kernelIndex, outputDimensions.x / blockSize.x, outputDimensions.y / blockSize.y, blockSize.z);

        // mark as done
        performedCompute = true;
    }


    // stuff be overriden in derived classes
    public virtual void AssignComputeBuffers(int kernelIndex){
        computeShader.SetTexture(kernelIndex, ouputName, outputTexture);

        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);
    }
    public virtual void UpdatePreview(){
        // ...
    }

    public void SetShowing(bool shouldShow){
        showingOnPreview = shouldShow;
    }
    public string GetKernelName(){
        return computeShaderKernel;
    }
}
