using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SlotDirrector : MonoBehaviour
{
    public Sprite[] samples;
    public Transform[] slots;
    public Image[] targetImages;
    void Start()
    {
        
    }

    void ReceiveDirrection()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

        }
    }
    public IEnumerator SwapAnims()
    {
        yield return null;
    }
    //최종루프엔 
}
