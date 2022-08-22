using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Liminal.SDK;

public class PointerExample : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
    
    
        public void OnPointerClick(PointerEventData eventData)
        {
            Destroy(gameObject);
        }
        
    

    // Update is called once per frame
   
}
