using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FChose : AFState
{
    protected readonly ChosePanelB panel;



    public FChose(StateMashine rootMashine,ChosePanelB inpanel) : base(rootMashine)
    {
        panel = inpanel;
    }

    public override void Enter()
    {
        panel.gameObject.SetActive(false);
        panel.PrintPanel();
    }
    public override void Exit()
    {
        panel.gameObject.SetActive(true);
    }
}
