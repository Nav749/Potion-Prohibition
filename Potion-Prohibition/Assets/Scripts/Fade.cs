using UnityEngine;

public class Fade : MonoBehaviour
{
    private Animator Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Animator = GetComponent<Animator>();
        ResetAnim();
    }

    public void FadeIn()
    {
        Animator.SetTrigger("Fade");
    }

    public void FadeOut()
    {
        Animator.SetTrigger("FadeOut");
    }

    public void ResetAnim()
    {
        Animator.SetTrigger("Reset");
    }
}
