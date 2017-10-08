using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class AnimatedText: BaseMeshEffect
{
    [SerializeField] private float _waitBetweenLetters = .1f;
    private CanvasRenderer _canvasRendered;

    public event System.Action OnFinish;
    private Mesh _mesh;

    private List<UIVertex> original = new List<UIVertex>();
    private List<UIVertex> modified = new List<UIVertex>();

    private Text _label;

    private bool _canWait = true;
    public bool IsWorking { get; private set; }

    protected override void Awake()
    {
        _canvasRendered = GetComponent<CanvasRenderer>( );
        _mesh = new Mesh( );
        _label = GetComponent<Text>( );
    }

    public void ForceFinish()
    {
        _canWait = false;
    }

    public override void ModifyMesh(VertexHelper vh)
    {
        if( !Application.isPlaying )
            return;
        StopAllCoroutines( );
        vh.GetUIVertexStream( original );
        modified = new List<UIVertex>( original );
        StartCoroutine( Write( vh ) );
    }

    private void HideLetters(VertexHelper vh)
    {
        var lenght = original.Count;
        var zero = Vector3.zero;
        for( var i = 0; i < lenght; ++i )
        {
            var uiVer = modified[i];
            uiVer.position = zero;
            modified[i] = uiVer;
        }
        vh.AddUIVertexTriangleStream( modified );
        vh.FillMesh( _mesh );
        _canvasRendered.SetMesh( _mesh );
    }

    private IEnumerator Write(VertexHelper vh)
    {
        IsWorking = true;

        _canWait = true;
        HideLetters( vh );
        var waitforLetter = new WaitForSeconds(this._waitBetweenLetters);
        var lenght = original.Count / 6;
        for( var i = 0; i < lenght; ++i )
        {
            for( var j = 0; j < 6; ++j )
            {
                var index = ( i * 6 ) + j;
                var tmpVer = modified[index];
                tmpVer.position = original[index].position;
                modified[index] = tmpVer;
            }
            vh.AddUIVertexTriangleStream( modified );
            vh.FillMesh( _mesh );
            _canvasRendered.SetMesh( _mesh );
            if( _canWait )
            {
                yield return waitforLetter;
            }
        }
        IsWorking = false;
        if(OnFinish != null)
        {
            OnFinish.Invoke( );
        }
        yield return null;
    }
}