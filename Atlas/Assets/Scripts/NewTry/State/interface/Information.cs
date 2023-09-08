

using UnityEngine;

public class Information : IState
{
    protected readonly Canvas pano;
    public Information(Canvas pano)
    {
        this.pano = pano;
        this.pano.gameObject.SetActive(false);
    }

    public Information()
    {
        this.pano = null;
        this.pano.gameObject.SetActive(false);
    }

    public void Enter() {
        this.pano.gameObject.SetActive(true);

    }

    public void Exit()
    {
        this.pano.gameObject.SetActive(false);
    }

    public void Update()
    {
        
    }
}
