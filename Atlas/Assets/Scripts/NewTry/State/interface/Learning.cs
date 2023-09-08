using UnityEngine;

internal class Learning : IState
{


    protected readonly Canvas pano;
    public Learning(Canvas pano)
    {
        this.pano = pano;
        this.pano.gameObject.SetActive(false);
    }

    public Learning()
    {
        this.pano = null;
    }
    public void Enter()
    {
        this.pano.gameObject.SetActive(true);

    }

    public void Exit()
    {
        this.pano.gameObject.SetActive(false);
    }

    public void Update()
    {}
}