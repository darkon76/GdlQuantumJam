using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSprite : MonoBehaviour {

    [SerializeField]
    Sprite[] _sprites;
	// Update is called once per frame
	void Awake ()
    {
        if( _sprites.Length == 0 )
        {
            Destroy( this );
            return;
        }
        var random = Random.Range(0, _sprites.Length);
        var spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = _sprites[random];
        Destroy( this );
	}
}
