using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pseudo3DSorterController : MonoBehaviour
{
    [Range(0.1f, 5f)]
    public float BaseElevation;
    [Range(0.1f, 5f)]
    public float SortSpacing;
    public List<GameObject> Everything;

    private GameObject _swapBuffer;

	void Start () {
	}
	
	
	void Update () {
        // Single iteration of setting objects in the list in correct order
	    for(int i = 0; i < Everything.Count - 1; ++i)
	    {
	        if (Everything[i].transform.position.y < Everything[i + 1].transform.position.y)
	        {
	            _swapBuffer = Everything[i];
	            Everything[i] = Everything[i + 1];
                Everything[i + 1] = _swapBuffer;
	        }
	    }
        // Applying corrent rendering order via sprite renderer sortingOrder
	    int i1 = 0;
	    foreach (var obj in Everything)
	    {
	        GameObject subj = obj.transform.parent.gameObject;
            SpriteRenderer[] children = subj.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in children)
                sprite.sortingOrder = i1;
            i1++;
        }

	    //foreach (var obj in Everything)
	    //    Debug.Log(obj.transform.position);
	    

    }
}
