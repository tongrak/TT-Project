using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Filp : MonoBehaviour
{

    private SpriteRenderer rend;

    [SerializeField]
    private Sprite faceSprite, backSprite;

    private bool coroutineAllowed, facedUp;

    

    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        rend.sprite = backSprite;
        coroutineAllowed = true;
        facedUp = false;
    }


    private void OnMouseDown()
    {
        if(coroutineAllowed)
        {
            StartCoroutine(RotateCard());
        }
    }
    
    private IEnumerator RotateCard()
    {
        coroutineAllowed = false;
        
        if(!facedUp)
        {
            for(float i = 0f; i <= 180f; i += 10f)
            {
                transform.rotation = Quaternion.Euler(0f,i, 0f);
                if(i == 90f)
                {
                    rend.sprite = faceSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }

        }
        
        else if(facedUp)
        {
            for (float i = 180f; i >= 0f; i -= 10f)
            {
                transform.rotation = Quaternion.Euler(0f, i, 0f);
                if (i == 90f)
                {
                    rend.sprite = backSprite;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }

        coroutineAllowed = true;

        facedUp = !facedUp;
    }
}
