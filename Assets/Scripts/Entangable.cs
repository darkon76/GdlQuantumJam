using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Entangable : MonoBehaviour, IPointerClickHandler
{
    SpriteRenderer _spriteRenderer;

    [SerializeField]
    Sprite _normalSprite;
    [SerializeField]
    Sprite _entangledParentSprite;
    [SerializeField]
    Sprite _entangledChildSprite;

    private int _entangled;

    public int Entangled
    {
        get
        {
            return _entangled;
        }
        set
        {
            _entangled = value;
            switch(_entangled)
            {
                case 0:
                    _spriteRenderer.sprite = _normalSprite;
                    break;
                case 1:
                    _spriteRenderer.sprite = _entangledParentSprite;
                    break;
                case 2:
                    _spriteRenderer.sprite = _entangledChildSprite;
                    break;
            }
        }
    }
    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

    }
    private void OnDisable()
    {
        EntangleManager.Instance.Remove( this );
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != 0)
            return;
        TryEntangle( );
    }

    public void TryEntangle()
    {
        if( _entangled != 0 )
        {
            Entangled = 0;
            EntangleManager.Instance.Remove( this );
            return;
        }
        Entangled = EntangleManager.Instance.TryEntangle( this );
    }

}
