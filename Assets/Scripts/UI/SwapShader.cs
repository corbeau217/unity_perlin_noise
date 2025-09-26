using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwapShader : MonoBehaviour
{
    public TMP_Text currentShaderLabel;
    [Space(10)]
    [Tooltip("the shaders to swap between")]
    public List<ShaderManager> shaderManagers = new List<ShaderManager>();
    public int currentShader = 0;

    void Start(){}
    void Update(){
        for(int i = 0; i < shaderManagers.Count; i++){
            // only show the shader that should be shown
            shaderManagers[i].SetShowing( i==currentShader );
            if( i==currentShader ){
                currentShaderLabel.text = shaderManagers[i].GetKernelName();
            }
        }
    }
    public void SetCurrentShaderIndex(int index){
        if((index>=0)&&(index < shaderManagers.Count)){
            currentShader = index;
        }
    }
    public void NextShader(){
        currentShader = (currentShader+1)%shaderManagers.Count;
    }
    public void PrevShader(){
        // add count to it and modulo incase we're negative
        currentShader = (shaderManagers.Count + (currentShader-1))%shaderManagers.Count;
    }
}
