using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class Controller : MonoBehaviour
{
    private Material material;
    public Material thisMat;
    private Matrix4x4 matrix_transform;
    public GameObject pfb;
    public GameObject hole;
    public RenderTexture renderTexture;
    public RenderTexture renderTexture1;

    private void Start()
    {
        GameObject backgroundPlane = GameObject.Find("BackgroundPlane");
        material = backgroundPlane.GetComponent<MeshRenderer>().material;

        thisMat = this.GetComponent<MeshRenderer>().material;

        Matrix4x4 matrix_R = Matrix4x4.Rotate(Quaternion.Euler(0, 0, -180));
        Matrix4x4 matrix_T = Matrix4x4.Translate(new Vector3(1, 1, 0));
        matrix_transform = matrix_T * matrix_R;
        thisMat.SetMatrix("_ReverseMatrix", matrix_transform);
    }

    private void Update()
    {
       
    }

    private bool flag = false;

    public void Click()
    {
        if (!flag)
        {
            Graphics.Blit(new Texture(), renderTexture, material);
            Graphics.Blit(new Texture(), renderTexture1, thisMat);
            flag = true;
        }
        hole.GetComponentInChildren<Test>().Explode(renderTexture1);
    }


    private IEnumerator C_Update()
    {
        yield return 0;
        while (flag)
        {
            //yield return new WaitForEndOfFrame();
            yield return 0;
#if UNITY_EDITOR
            if (Input.GetMouseButton(0))
#elif UNITY_ANDROID || UNITY_IPHONE
            if(Input.touchCount==1)
#endif
            {
#if UNITY_EDITOR
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Vector2 cursorPoint = Input.mousePosition;
#elif UNITY_ANDROID || UNITY_IPHONE
                Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                Vector2 cursorPoint = Input.GetTouch(0).position;
#endif
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "A")
                    {
                        flag = false;
                        RenderTexture renderTexture = new RenderTexture(1920, 1080, 24);
                        Graphics.Blit(new Texture(), renderTexture, material);

                        GameObject hole = Instantiate(pfb, hit.point, Quaternion.identity);
                        hole.GetComponentInChildren<Test>().Explode(renderTexture);

                        yield return new WaitForSeconds(1);
                    }
                }
            }
        }
    }

}
