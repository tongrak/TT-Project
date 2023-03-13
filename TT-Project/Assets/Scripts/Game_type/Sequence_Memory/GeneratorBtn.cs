using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GeneratorBtn : MonoBehaviour
{
    [SerializeField]
    private Transform gameField;

    [SerializeField]
    private Transform img;

    [SerializeField]
    private GameObject btns;

    [SerializeField]
    private GameObject btns2;

    [SerializeField]
    private GameObject btns22;

    [SerializeField]
    private GameObject btns3;

    public int level;

    //DDA
    [SerializeField]
    private DDA DDA;

    [SerializeField]
    private GameObject quad;

    void Awake()
    {
        //int max = Random.Range(4, 6);
        //MeshCollider c = quad.GetComponent<MeshCollider>();
        //float screenX, screenY;
        Vector3 pos;
        
        if(DDA.Level == 1)
        {
            img.GetComponent<Image>().color = new Color((float)0.6039216, (float)0.9764706, (float)0.4705882, 1);
          

            for (int i = 0; i < 4; i++)
            {
                
                int[] x = {-100, 100, -100, 100 };
                int[] y = { 100, 100, -100, -100 };

                Color[] color = { new Color(1, 0, 0, 0.5f), new Color(0, 0, 1, 0.5f), new Color(1, 1, 0, 0.5f), new Color(0, 1, 0, 0.5f) };

                
                pos = new Vector3(x[i], y[i], -99);

                //Color color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f), 0.5f);

                GameObject button = Instantiate(btns, pos, btns.transform.rotation);
                button.GetComponent<SpriteRenderer>().color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
                button.name = "" + i;


                button.transform.SetParent(gameField, false);


            }
        }
        else if(DDA.Level == 2)
        {
            img.GetComponent<Image>().color = new Color((float)0.9764706, (float)0.8941177, (float)0.4705882, 1);
            int count = 1;
            for (int i = 0; i < 7; i++)
            {
                
                int[] x = { -330, -220,-110,0,110, 220, 330 };
                int[] y = { -20, 20, -20, 20,-20,20,-20 };

                Color[] color = { new Color(1, 0, 0, 0.5f), new Color(0, 0, 1, 0.5f), new Color(1, 1, 0, 0.5f), new Color(0, 1, 0, 0.5f), new Color(249, 120, 224, 0.5f), new Color(33, 236, 65, 0.5f) };

                
                pos = new Vector3(x[i], y[i], -99);

                

                if(count % 2 == 0)
                {
                    //btns2.transform.eulerAngles = new Vector3(0,0,btns2.transform.eulerAngles.z + 180);
                    GameObject button = Instantiate(btns22, pos, btns22.transform.rotation);
                    button.GetComponent<SpriteRenderer>().color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
                    button.name = "" + i;
                    button.transform.SetParent(gameField, false);
                }
                else
                {
                    
                    GameObject button = Instantiate(btns2, pos, btns2.transform.rotation);
                    button.GetComponent<SpriteRenderer>().color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
                    button.name = "" + i;

                    button.transform.SetParent(gameField, false);
                }

                
                count++;
            }
        }
        else if(DDA.Level == 3)
        {
            img.GetComponent<Image>().color = new Color((float)0.9372549, (float)0.3490196, (float)0.2666667, 1);

            for (int i = 0; i < 10; i++)
            {
                int[] x = { -402, -363, -325, -20, -202, 122, 213,343,-24,363 };
                int[] y = { -267, 226, 20, 150, 96, -210, -62,2,269,-128 };

                Color[] color = { new Color(1, 0, 0, 0.5f), new Color(0, 0, 1, 0.5f), new Color(1, 1, 0, 0.5f), new Color(0, 1, 0, 0.5f), new Color(249, 120, 224, 0.5f), new Color(33, 236, 65, 0.5f) };
                
                pos = new Vector3(x[i], y[i], -99);
                GameObject button = Instantiate(btns3, pos, btns3.transform.rotation);
                button.GetComponent<SpriteRenderer>().color = new Color((float)0.1176471, (float)0.1176471, (float)0.1176471, 1);
                button.name = "" + i;

                button.transform.SetParent(gameField, false);


            }
        }

        

    }
}//AddButtons
