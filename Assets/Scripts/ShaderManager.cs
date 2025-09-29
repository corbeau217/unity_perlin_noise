using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShaderManager : MonoBehaviour
{

    // =============================
    // ===== helper values

    // used for giving shaders a reference and value to use in boolean operations
    public static int trueIntValue = 1;
    public static int falseIntValue = 0;

    // =============================

    public ComputeShader computeShader;
    public Vector3Int blockSize = new Vector3Int(16,16,1);
    public string computeShaderKernel = "WhiteNoise1";
    [Space(10)]
    public RenderTexture outputTexture;
    public Vector2Int outputDimensions = Vector2Int.one * 256;
    public string ouputName = "ResultImage";
    public FilterMode outputFilterMode = FilterMode.Bilinear;
    [Space(10)]
    public bool performedCompute = false;
    public bool showingOnPreview = false;
    [Tooltip("set this to true in most cases unless you want the shader to wait on something before performing its compute shader")]
    public bool safeToPerform = false;
    
    void Start(){
        OverrideShaderKernelName();
        SettingOverrides();
        this.PrepareRenderTextures();
    }
    void Update(){
        TestReadyToPerform();
        if(!performedCompute && safeToPerform){
            this.PerformComputeShader();
        }
        if(showingOnPreview && safeToPerform){
            this.UpdatePreview();
        }
    }
    void OnDestroy(){
        outputTexture?.Release();
        outputTexture = null;
    }

    private void PrepareRenderTextures(){
        this.outputTexture = new RenderTexture(outputDimensions.x, outputDimensions.y, 0);
        this.outputTexture.filterMode = outputFilterMode;
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
    public virtual void OverrideShaderKernelName(){
        // ...
    }
    public virtual void SettingOverrides(){
        // ...
    }
    public virtual void AssignComputeBuffers(int kernelIndex){
        computeShader.SetTexture(kernelIndex, ouputName, outputTexture);

        computeShader.SetInt("textureWidth", outputDimensions.x);
        computeShader.SetInt("textureHeight", outputDimensions.y);
    }
    public virtual void UpdatePreview(){
        // ...
    }
    // override this with other behaviour if needed
    public virtual void TestReadyToPerform(){
        // TODO : check if the compute shader is supported
        safeToPerform = true;
    }

    public void SetShowing(bool shouldShow){
        showingOnPreview = shouldShow;
    }
    public string GetKernelName(){
        return computeShaderKernel;
    }
    public bool HasPerformedCompute(){
        return performedCompute;
    }
    public RenderTexture GetResultTexture(){
        return outputTexture;
    }
    public Vector2Int GetOutputDimensions(){
        return outputDimensions;
    }
}
