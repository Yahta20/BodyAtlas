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
        panel.gameObject.SetActive(true);
        panel.FillText(new string[] {
            ContentLoc.Instance.GetLocalText("grata verbum"),
            ContentLoc.Instance.GetLocalText("audit"),
            ContentLoc.Instance.GetLocalText("adsuescere")
        });
        

        panel.SetIplementing(
             () => { rootMashine.SetState<FAudit>(); },//left button 
             () => { rootMashine.SetState<FStudy>(); } //right button
                 //rootMashine.SetState<>()
             );
        Control.Instance.VisibilityOfPreparat(false);
        /*
         */
        //panel.PrintPanel(CanvasBehavior.Instance.getSize());
    }
    public override void Exit()
    {
        panel.gameObject.SetActive(false);
    }
}
