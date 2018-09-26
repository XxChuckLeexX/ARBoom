using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private new Rigidbody rigidbody;

    private void Start()
    {
         rigidbody = GetComponent<Rigidbody>();
    }
    private bool flag = false;

    public void Explode(RenderTexture renderTexture)
    {
        if (!flag)
        {
            flag = true;
            GetComponent<MeshRenderer>().material = new Material(Shader.Find("Unlit/Test"));
            Material material = GetComponent<MeshRenderer>().material;
            Matrix4x4 matrix_MVP = Camera.main.projectionMatrix * Camera.main.worldToCameraMatrix * transform.localToWorldMatrix;
            material.SetMatrix("_FixedMVP", matrix_MVP);
            material.SetTexture("_MainTex", renderTexture);
            
            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
        }
        
        rigidbody.AddExplosionForce(100, transform.position - new Vector3(1,1,0), 50);
    }

    public void Reset()
    {
        
    }

}
