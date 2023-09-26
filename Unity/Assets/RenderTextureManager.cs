using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderTextureManager : MonoBehaviour
{
    public GameObject m_Go;
    // Start is called before the first frame update
    void Start()
    {
        var m_RT = m_Go.GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
