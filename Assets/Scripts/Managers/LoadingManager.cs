using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadingManager : MonoBehaviour
{
    [SerializeField]
    float _fadeTime;


    [SerializeField]
    Material _loadingMaterial;
    static readonly int cutOff = Shader.PropertyToID("_Cutoff");
    [SerializeField]
    int currentScene = -1;
    [SerializeField]
    int maxLevels = 5;
    bool _loading = false;

    public event Action OnLoadFinish;
    public static LoadingManager Instance;

    private void Awake()
    {
        Instance = this;
        _loadingMaterial.SetFloat( cutOff, 0 );
        
    }

#if !UNITY_EDITOR
    private void Start()
    {
        currentScene = 0;
        LoadLevel( 1 );
    }
#endif
    public void LoadLevel(int value)
    {
        if( !_loading)
            StartCoroutine( Transition( value ) );
       
    }
    public void LoadNext()
    {
        if( !_loading )
            StartCoroutine( Transition( currentScene + 1 ) );
    }

    public void Restart()
    {
        if( !_loading )
            StartCoroutine( Transition( currentScene ) );
    }
    private IEnumerator Transition(int newLevel)
    {
        _loading = true;
        var timer =0f;
        while(timer < _fadeTime)
        {
            timer += Time.deltaTime;
            var percentage = timer /_fadeTime;
            _loadingMaterial.SetFloat( cutOff, percentage );
            yield return null;
        }
        if( currentScene != 0 )
        {
            yield return SceneManager.UnloadSceneAsync( currentScene );
        }
        yield return SceneManager.LoadSceneAsync( newLevel, LoadSceneMode.Additive );
        currentScene = newLevel;
        timer = 0f;
        while( timer < _fadeTime )
        {
            timer += Time.deltaTime;
            var val = (timer /_fadeTime);
            var fade2 = 1 - val;
            _loadingMaterial.SetFloat( cutOff, fade2 );
            yield return null;
        }
        _loadingMaterial.SetFloat( cutOff, 0 );
        _loading = false;
        if( OnLoadFinish != null )
            OnLoadFinish( );
    }
    private void OnDestroy()
    {
        _loadingMaterial.SetFloat( cutOff, 0 );
    }
}
