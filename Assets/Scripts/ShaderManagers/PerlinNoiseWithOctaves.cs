using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PerlinNoiseWithOctaves : ShaderManager
{
    [Space(30)]
    [Tooltip("manually chosen place to source our noise")]
    public ShaderManager inputNoiseManager;
    [Tooltip("manually chosen location to show our preview")]
    public RawImage previewImage;
    [Space(10)]
    [Tooltip("this is retreived by the shader manager at runtime")]
    public RenderTexture inputNoiseTexture;
    [Tooltip("this is retreived by the shader manager at runtime")]
    public Vector2Int inputDimensions;
    [Space(30)]
    public Vector2Int gridCellNoiseOrigin1 = Vector2Int.zero;
    public Vector2Int gridCellCount1 = Vector2Int.one * 4;
    public Matrix4x4 gridCellUVMatrix1 = Matrix4x4.identity;
    public float octaveContribution1 = 1.0f;
    [Space(15)]
    public Vector2Int gridCellNoiseOrigin2 = Vector2Int.zero;
    public Vector2Int gridCellCount2 = Vector2Int.one * 8;
    public Matrix4x4 gridCellUVMatrix2 = Matrix4x4.identity;
    public float octaveContribution2 = 0.5f;
    [Space(15)]
    public Vector2Int gridCellNoiseOrigin3 = Vector2Int.zero;
    public Vector2Int gridCellCount3 = Vector2Int.one * 16;
    public Matrix4x4 gridCellUVMatrix3 = Matrix4x4.identity;
    public float octaveContribution3 = 0.25f;
    [Space(15)]
    public Vector2Int gridCellNoiseOrigin4 = Vector2Int.zero;
    public Vector2Int gridCellCount4 = Vector2Int.one * 16;
    public Matrix4x4 gridCellUVMatrix4 = Matrix4x4.identity;
    public float octaveContribution4 = 0.125f;

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

        UpdateUVMatrices();

        float[] cellCountsArray1 = gridCellCount1.ToFloatArray();
        float[] cellCountsArray2 = gridCellCount2.ToFloatArray();
        float[] cellCountsArray3 = gridCellCount3.ToFloatArray();
        float[] cellCountsArray4 = gridCellCount4.ToFloatArray();
        
        computeShader.SetMatrix("octaveUVMatrix1", gridCellUVMatrix1);
        computeShader.SetMatrix("octaveUVMatrix2", gridCellUVMatrix2);
        computeShader.SetMatrix("octaveUVMatrix3", gridCellUVMatrix3);
        computeShader.SetMatrix("octaveUVMatrix4", gridCellUVMatrix4);

        computeShader.SetFloats("cellCounts1", cellCountsArray1);
        computeShader.SetFloats("cellCounts2", cellCountsArray2);
        computeShader.SetFloats("cellCounts3", cellCountsArray3);
        computeShader.SetFloats("cellCounts3", cellCountsArray3);
        computeShader.SetFloats("cellCounts4", cellCountsArray4);

        computeShader.SetFloat("octaveContribution1", octaveContribution1);
        computeShader.SetFloat("octaveContribution2", octaveContribution2);
        computeShader.SetFloat("octaveContribution3", octaveContribution3);
        computeShader.SetFloat("octaveContribution3", octaveContribution3);
        computeShader.SetFloat("octaveContribution4", octaveContribution4);

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

    public void UpdateUVMatrices(){
        // use our helper function in Util.cs
        //  to just make it a little tidier
        gridCellUVMatrix1 = gridCellUVMatrix1.UpdateTranslationScale2DInt( gridCellNoiseOrigin1, gridCellCount1 );
        gridCellUVMatrix2 = gridCellUVMatrix2.UpdateTranslationScale2DInt( gridCellNoiseOrigin2, gridCellCount2 );
        gridCellUVMatrix3 = gridCellUVMatrix3.UpdateTranslationScale2DInt( gridCellNoiseOrigin3, gridCellCount3 );
        gridCellUVMatrix4 = gridCellUVMatrix4.UpdateTranslationScale2DInt( gridCellNoiseOrigin4, gridCellCount4 );
    }
}
