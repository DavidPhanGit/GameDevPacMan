using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    [SerializeField] private GameObject fish;
    [SerializeField] private AudioSource movingAudio;
    private Transform fishTransform;
    private Animator fishAnim;
    private List<Tween> activeTweens;
    private int currentTweenIndex;
    // Start is called before the first frame update
    void Start()
    {
        fishTransform = fish.GetComponent<Transform>();
        fishAnim = fish.GetComponent<Animator>();
        activeTweens = new List<Tween>();
        currentTweenIndex = 0;
        activeTweens.Add(new Tween(fishTransform, new Vector3(1.0f, -1.0f, 0.0f), new Vector3(6.0f, -1.0f, 0.0f), Time.time, 2.0f));
        activeTweens.Add(new Tween(fishTransform, new Vector3(6.0f, -1.0f, 0.0f), new Vector3(6.0f, -5.0f, 0.0f), Time.time, 2.0f));
        activeTweens.Add(new Tween(fishTransform, new Vector3(6.0f, -5.0f, 0.0f), new Vector3(1.0f, -5.0f, 0.0f), Time.time, 2.0f));
        activeTweens.Add(new Tween(fishTransform, new Vector3(1.0f, -5.0f, 0.0f), new Vector3(1.0f, -1.0f, 0.0f), Time.time, 2.0f));
        //StartCoroutine(moveTween(activeTweens[currentTweenIndex]));
        movingAudio.Play();
        movingAudio.loop = true;
    }

    // Update is called once per frame
    void Update()
    {

        if (currentTweenIndex < activeTweens.Count)
        {
            Tween activeTween = activeTweens[currentTweenIndex];
            float timeFraction = (Time.time - activeTween.StartTime) / activeTween.Duration;

            if (timeFraction < 1.0f)
            {
                // Tween is still in progress, perform the lerp
                activeTween.Target.position = Vector3.Lerp(activeTween.StartPos, activeTween.EndPos, timeFraction);
            }
            else
            {
                // Tween is done, move to the next tween
                currentTweenIndex++;
                fishAnim.SetInteger("Direction", currentTweenIndex);
                if (currentTweenIndex < activeTweens.Count)
                {
                    // Reset start time for the next tween
                    activeTweens[currentTweenIndex].StartTime = Time.time;
                }
            }
        }

    }

}
