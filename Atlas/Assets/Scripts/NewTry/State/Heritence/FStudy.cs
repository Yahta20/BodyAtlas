using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FStudy : AFState
{
    protected readonly LernPanelB lpanel;

    public FStudy(StateMashine rootMashine,LernPanelB Lpanel) : base(rootMashine)
    {
        lpanel = Lpanel;
        lpanel.SetExtImplemets(() => { rootMashine.SetState<FChose>(); });
    }
    public override void Enter() { 
        Control.Instance.VisibilityOfPreparat(true);
        lpanel.gameObject.SetActive(true);
    }

    public override void Exit() {
        lpanel.gameObject.SetActive(false);
        Control.Instance.HideIndicator();
        Control.Instance.VisibilityOfPreparat(false);
    }

}
