using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FAudit : AFState
{
    protected readonly ChosePanelB panel;
    protected readonly AuditPanelB  auditP;
    protected readonly ResultPanelB resultP;

    //protected readonly ChosePanelB panel;
    //protected readonly ChosePanelB panel;
    protected readonly StateMashine slaveMashine;

    public FAudit(StateMashine rootMashine, ChosePanelB panel, AuditPanelB audit, ResultPanelB result) : base(rootMashine)
    {
        this.panel = panel;
        this.slaveMashine = new();
        auditP = audit;
        resultP =result;
        slaveMashine.AddState(new FAuditQutig   (slaveMashine, this.panel));
        slaveMashine.AddState(new FAuditIntervue(slaveMashine, auditP));
        slaveMashine.AddState(new FAuditResult  (slaveMashine, resultP));
    }






    public override void Enter()
    {
        //panel.gameObject.SetActive(false);
        Control.Instance.VisibilityOfPreparat(true);
        slaveMashine.SetState<FAuditIntervue>();

    }
           


    public override void Exit()
    {
       // panel.gameObject.SetActive(false);
    }

    public override void Update()
    {
        slaveMashine.Update();
    }

    

    public class FAuditIntervue : AFState
    {
        protected readonly AuditPanelB auditP;

        public FAuditIntervue(StateMashine rootMashine, AuditPanelB audit) : base(rootMashine)
        {
            auditP = audit;
        }
        public override void Enter()
        {
            auditP.gameObject.SetActive(true);
            
        }

        public override void Exit()
        {
            auditP.gameObject.SetActive(false);
        }
    }
    public class FAuditResult : AFState
    {

        protected readonly ResultPanelB resultP;
        public FAuditResult(StateMashine rootMashine, ResultPanelB resultP) : base(rootMashine)
        {
            this.resultP = resultP;
        }
        public override void Enter()
        {
            resultP.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            resultP.gameObject.SetActive(false);
        }
    }
    public class FAuditQutig: AFState
    {
        protected readonly ChosePanelB panel;
        public FAuditQutig(StateMashine rootMashine,ChosePanelB panel) : base(rootMashine)
        {
            this.panel = panel;
        }
        public override void Enter()
        {
            panel.gameObject.SetActive(true);
        }

        public override void Exit()
        {
            panel.gameObject.SetActive(false);
        }

    }



}


