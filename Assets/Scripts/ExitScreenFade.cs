using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Liminal.SDK.Core;
using Liminal.Core.Fader;

public class ExitScreenFade :FlowerGrowth
{
    float fadeDuration = 0.5f;
    Color color = Color.black;
    public int numberOfBloomedFlower;
    [SerializeField]
    List<GameObject> flowers;



    // Start is called before the first frame update
    void Start()
    {
                
    }

    //add trigger to call expEnd function
    void expEnd()
    {
        var fader = ScreenFader.Instance;
        fader.FadeTo(color, fadeDuration);
        ExperienceApp.End();
    }
    // Update is called once per frame
    void Update()
    {
        numberOfBloomedFlower = statFlowerBlooms;
        if (numberOfBloomedFlower >= 10)
        {
            Invoke("expEnd", 30);
        }

        /*foreach(GameObject gameObject in flowers)
        {
            if (gameObject.GetComponent<Animator>().GetBool("isBloomed"))
            {
                numberOfBloomedFlower+= 1;

                return;
            }

        }*/

        /*for (int i = flowers.Count - 1; i >= 0; i--)
        {
            if (flowers[i].GetComponent<Animator>().GetBool("isBloomed"))
            {
                numberOfBloomedFlower += 1;

            }
        }*/
    }
}

