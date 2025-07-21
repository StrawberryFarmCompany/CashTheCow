using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class SlotDirrector : MonoBehaviour
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

    void ReceiveDirrection(int index)
    {
        int temp = new System.Random().Next(1, 5);
        StartCoroutine(SwapAnims(index, temp,3f));
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            results[0] = UnityEngine.Random.Range(0, samples.Length);
            results[1] = UnityEngine.Random.Range(0, samples.Length);
            results[2] = UnityEngine.Random.Range(0, samples.Length);
            ReceiveDirrection(0);
            ReceiveDirrection(1);
            ReceiveDirrection(2);
        }
    }
    public IEnumerator SwapAnims(int index, int loopTimes,float time)
    {
        int offSet = results[index]+(loopTimes * samples.Length);
        int currIndex = 0;
        int idxOne = index * 2;
        int idxTwo = index * 2 + 1;

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
            currIndex+=2;

            float tempHei = height * -2f;//

            if (offSet <= 0) break;

            firstSequences.Append(dirrSlot[idxOne].rectTransform.DOLocalMoveY(tempHei, 0.3f));

            secondSequences.Append(dirrSlot[idxTwo].rectTransform.DOLocalMoveY(tempHei, 0.3f));

            firstSequences.AppendCallback(() => { dirrSlot[idxOne].rectTransform.localPosition = Vector3.zero; dirrSlot[idxOne].sprite = samples[firstSample]; });

            secondSequences.AppendCallback(() => { dirrSlot[idxTwo].rectTransform.localPosition = Vector3.zero; dirrSlot[idxTwo].sprite = samples[secondSample]; });

        }
        resultImages[index].sprite = samples[results[index]];
        firstSequences.Append(resultImages[index].rectTransform.DOLocalMoveY(-height,0.3f));
        firstSequences.Play();
        yield return new WaitForSeconds(0.15f);
        secondSequences.Play();
        secondSequences.Play();
    }
    //최종루프엔 
}
