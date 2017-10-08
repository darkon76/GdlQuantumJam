using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : MonoBehaviour {

    LoadingManager _loadingManager;
	// Use this for initialization
	void Start () {
        _loadingManager = FindObjectOfType<LoadingManager>( );
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            var col = GetComponent<Collider2D>();
            col.enabled = false;
            StartCoroutine( LoadNextLevel() );
        }
    }

    IEnumerator LoadNextLevel()
    {
       // yield return new WaitForSeconds(2f);
        _loadingManager.LoadNext( );
        yield return null;
    }
}
