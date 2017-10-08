using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceEnTangle : MonoBehaviour {

    [SerializeField]
    Entangable Parent;
    [SerializeField]
    Entangable Child;
	// Use this for initialization
	void Start () {
        var manager = FindObjectOfType<EntangleManager>();
        Parent.TryEntangle( );
        Child.TryEntangle( );
        Destroy( this );
	}
	

}
