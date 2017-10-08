using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionTest : MonoBehaviour {

    [SerializeField]
    Material _loadingMaterial;
    static readonly int cutOff = Shader.PropertyToID("Cutoff");
    [Range(0,1)]
    public float _cutOff;
    [SerializeField]
    float shaderValue;

    private void Awake()
    {
        var blit = FindObjectOfType<SimpleBlit>();
        _loadingMaterial = blit.TransitionMaterial;
    }

    private void OnValidate()
    {
        if( Application.isPlaying )
        {
            _loadingMaterial.SetFloat( "Cutoff", _cutOff );
            shaderValue = _loadingMaterial.GetFloat( cutOff );
        }
    }
}
