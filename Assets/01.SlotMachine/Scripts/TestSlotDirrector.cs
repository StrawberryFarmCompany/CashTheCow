using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using UnityEditor.Timeline.Actions;
using System.Xml.Schema;

public class TestSlotDirrector : MonoBehaviour
{
    [SerializeField]private Sprite[] samples;
    [SerializeField] private Image[] dirrSlot;
    [SerializeField] private Image[] resultImages;

    [SerializeField] private int[] results;
    [SerializeField] private float height;
    
    void Start()
    {
        height = resultImages[0].rectTransform.sizeDelta.y;
    }

    void ReceiveDirrection(int index,int loop)
    {
        //int temp = new System.Random().Next(1, 5);
        StartCoroutine(SwapAnims(index, loop,3f));
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SlotSpin();
        }
    }
    public IEnumerator SwapAnims(int index, int loopTimes,float time)
    {
        int offSet = results[index]+(loopTimes * samples.Length);
        int currIndex = 0;
        int idxOne = index * 2;
        int idxTwo = index * 2 + 1;

        float invokeTime = 0;
        float currTime = 0;
        resultImages[index].rectTransform.localPosition = Vector3.zero;

        Sequence firstSequences = DOTween.Sequence();
        Sequence secondSequences = DOTween.Sequence();
        float timePerLoop = time / loopTimes;

        firstSequences.Pause();
        secondSequences.Pause();

        
        while (offSet > 0)
        {
            offSet -=2;
            int firstSample = currIndex% samples.Length;
            int secondSample = (currIndex + 1) % samples.Length;
            currIndex+= 2;
            float tempHei = height * -2f;//

            invokeTime += currTime;
            float eachTime = 0.1f;
            currTime += eachTime;

            if (offSet <= 0) break;

            firstSequences.Insert(invokeTime, dirrSlot[idxOne].rectTransform.DOLocalMoveY(tempHei, currTime));
            secondSequences.Insert(invokeTime + (currTime/ 2f), dirrSlot[idxTwo].rectTransform.DOLocalMoveY(tempHei, currTime));

            firstSequences.AppendCallback(() => { dirrSlot[idxOne].rectTransform.localPosition = Vector3.zero; dirrSlot[idxOne].sprite = samples[firstSample]; });

            secondSequences.AppendCallback(() => { dirrSlot[idxTwo].rectTransform.localPosition = Vector3.zero; dirrSlot[idxTwo].sprite = samples[secondSample]; });
        }
        firstSequences.SetEase(Ease.Unset);
        secondSequences.SetEase(Ease.Unset);
        resultImages[index].sprite = samples[results[index]];

        firstSequences.Append(resultImages[index].rectTransform.DOLocalMoveY(-height,currTime));

        firstSequences.Play();
        secondSequences.Play();
        yield return null;
    }
    //최종루프엔 
    public void SlotSpin()
    {
        if (BettingManager.Instance.IsRequireCredit())
        {
            results[0] = UnityEngine.Random.Range(0, samples.Length);
            results[1] = UnityEngine.Random.Range(0, samples.Length);
            results[2] = UnityEngine.Random.Range(0, samples.Length);
            ReceiveDirrection(0, 1);
            ReceiveDirrection(1, 2);
            ReceiveDirrection(2, 3);
            BettingManager.Instance.Spin();
        }
    }
}
