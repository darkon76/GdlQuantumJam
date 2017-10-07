using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntangleManager : MonoBehaviour {

    const int MaxEntangles = 2;

    public Entangable[] _entangled = new Entangable[MaxEntangles];
    private int counter = 0;

    private Rigidbody2D[] _rigids = new Rigidbody2D[MaxEntangles];
	// Update is called once per frame
	void FixedUpdate () {
        if(_entangled[0] != null && _entangled[1] != null)
        {
            _rigids[1].velocity = _rigids[0].velocity;
            _rigids[1].rotation = _rigids[0].rotation;
        }

	}

    void Update()//:D
    {
        if (Input.GetMouseButtonDown(1))
        {
            if(_entangled[0] != null)
            {
                _entangled[0].Entangled = 0;
                _entangled[0] = null;
            }
            if (_entangled[1] != null)
            {
                _entangled[1].Entangled = 0;
                _entangled[1] = null;
            }
            counter = 0;
        }
    }

    public void Remove(Entangable entangable)
    {
        if (_entangled[0] != null)
        {
            _entangled[0].Entangled = 0;
            _entangled[0] = null;
        }
        if (_entangled[1] != null)
        {
            _entangled[1].Entangled = 0;
            _entangled[1] = null;
        }
        counter = 0;
    }

    public int TryEntangle(Entangable entangable)
    {
        if (counter == MaxEntangles)
            return 0;
        _entangled[counter] = entangable;
        _rigids[counter] = entangable.GetComponent<Rigidbody2D>();
        counter++;
        return counter; 
        
    }
}
