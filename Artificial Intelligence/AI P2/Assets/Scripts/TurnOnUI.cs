using UnityEngine;

public class TurnOnUI : MonoBehaviour
{
    private Animator animator;
    public bool isAnimationFinished;

    [SerializeField] private GameObject ui;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!AnimatorIsPlaying()) {
            ui.SetActive(true);
        }
    }

    bool AnimatorIsPlaying(){
        return animator.GetCurrentAnimatorStateInfo(0).length > animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }

    bool AnimatorIsPlaying(string stateName){
        return AnimatorIsPlaying() && animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);
    }

    public void SetAnimationFinished()
    {
        isAnimationFinished = true;
    }
}
