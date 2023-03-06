using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LightObject : MonoBehaviour
{
    Animator animator;
    SceneScript sceneScript;
    private int ElectricCount = 0;
    private ElectricState state;
    private ElectricState lastPlayState;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sceneScript = GameObject.Find("end").GetComponent<SceneScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(sceneScript != null)
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Led-Broken")) {
                StartCoroutine(DelayFinishAnimate(animator.GetCurrentAnimatorStateInfo(0).length));
            }
            ElectricCount = sceneScript.CurrentElectric;
            if(ElectricCount <= 99)
            {
                state = ElectricState.Normal;
            } else if(ElectricCount > 199)
            {
                state = ElectricState.Broken;
            } else if(ElectricCount > 99)
            {
                state = ElectricState.Light;
            }
            PlayAnimate(state);
        }
    }

    IEnumerator DelayFinishAnimate(float delay)
    {
        yield return new WaitForSeconds(delay);
        sceneScript.RestartAnimation();
    }

    public void PlayAnimate(ElectricState state)
    {
        if (lastPlayState == null ||( lastPlayState != null && lastPlayState != state))
        {
            switch (state)
            {
                case ElectricState.Normal:
                    animator.Play("Led-Idle");
                    lastPlayState = ElectricState.Normal;
                    break;
                case ElectricState.Broken:
                    animator.Play("Led-Broken");
                    lastPlayState = ElectricState.Broken;
                    break;
                case ElectricState.Light:
                    animator.Play("Led-Light-On-Process");
                    lastPlayState = ElectricState.Light;
                    break;
            }
        }
    }

    public enum ElectricState
    {
        Normal,
        Broken,
        Light,
    }
}
