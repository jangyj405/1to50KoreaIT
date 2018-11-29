using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePool : MonoBehaviour
{
    public static CubePool instace = null;

    [SerializeField]
    private List<GameObject> EnabledBlock = new List<GameObject>();
    private Queue<GameObject> DisabledBlock = new Queue<GameObject>();

    void Awake()
    {
        instace = this;
    }

    public GameObject GetEmptyBlock()
    {
        if (DisabledBlock.Count == 0)
        {
            return null;
        }
        GameObject tBlock = null;
        tBlock = DisabledBlock.Dequeue();
        return tBlock;
    }
























	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}
}
