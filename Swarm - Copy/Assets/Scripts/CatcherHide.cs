using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CatcherHide : MonoBehaviour
{
    private bool seen = false;
   public void ISeeYou()
    {
        seen = true;
    }

    private void Update()
    {
        if (seen == true)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
        }
    }

}
