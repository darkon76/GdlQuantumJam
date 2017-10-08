using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPongHorizontal : MonoBehaviour {

    private enum Type
    {
        Horizontal = 0,
        Vertical = 1
    }
    [SerializeField]
    Type _type;
    private int axis;
    private int otherAxis;
    [SerializeField]
    float minPos = 0;
    [SerializeField]
    float maxPos = 1;
    [SerializeField]
    float Velocity = 10;

    [SerializeField]
    float _direction = 1f;

    Rigidbody2D _rigid;
    Vector2 _startPos;

    private void OnDrawGizmosSelected()
    {
        

        var tmpVec = transform.position;
        tmpVec[axis] = minPos;
        Gizmos.DrawSphere( tmpVec, .2f );
        tmpVec[axis] = maxPos;
        Gizmos.DrawSphere( tmpVec, .2f );

    }

    private void OnValidate()
    {
        axis = (int) _type;
        otherAxis = ( axis + 1 ) % 2;
    }

    // Use this for initialization
    void Awake () {
        _rigid = GetComponent<Rigidbody2D>( );
        _startPos = _rigid.position;
        axis = (int) _type;
        otherAxis = ( axis + 1 ) % 2;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        var position = _rigid.position;
        var posX = position[axis];
        if( posX > maxPos )
        {
            _direction = -1;
            position[axis] = maxPos;
        }
           
        else if( posX < minPos )
        {
            _direction = 1;
            position[axis] = minPos;
        }
         
        
        position[otherAxis] = _startPos[otherAxis];
        _rigid.position = position;
        
        var vel = _rigid.velocity;
        vel[axis] = Velocity * _direction;
        _rigid.velocity = vel;

	}
}
