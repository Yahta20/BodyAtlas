using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

[Serializable]
public class FAudit : AFState
{
    protected readonly ChosePanelB  panel;
    protected readonly AuditPanelB  auditP;
    protected readonly ResultPanelB resultP;
    //protected readonly StateMashine slaveMashine;

    Queue<Bone> Question;
    List<string> RAnswer;
    List<string> UAnswer;


    // = new Queue<Bone>();
    //protected readonly ChosePanelB panel;
    //protected readonly ChosePanelB panel;

    public FAudit(StateMashine rootMashine, ChosePanelB panel, AuditPanelB audit, ResultPanelB result) : base(rootMashine)
    {
        this.panel = panel;
        //this.slaveMashine = new();
        auditP = audit;
        resultP =result;
        CaseCreation();
        //slaveMashine.AddState(new FAuditQutig   (slaveMashine, this.panel));
        //slaveMashine.AddState(new FAuditIntervue(slaveMashine, auditP));
        //slaveMashine.AddState(new FAuditResult  (slaveMashine, resultP));
        //slaveMashine.SetState<FAuditIntervue>();

    }

    public void CaseCreation()
    {

        var a =Control.Instance.GetBoneArray(10);

        Question = new Queue<Bone>(a);
        
        /*
        UnityEngine.Debug.Log($"{Question.Count} - {a.Length}"); 

        var arr = Control.Instance.GetBoneArray(10);
        var rand = new Random();
        List<Bone> list = new List<Bone>();

        do
        {
            var ansv = Control.Instance.bones[rand.Next(Control.Instance.bones.Count)];
            if (ansv.gameObject.name.StartsWith("R_")|
                ansv.gameObject.name.StartsWith("L_")
                )
            {
                if (!list.Exists(p => p == ansv)|
                    !list.Exists(p => p.name == $"{ansv.gameObject.name.Substring(2)}\n"))
                {
                    list.Add(ansv);
                }
            }
        } while (list.Count>=10);
        Question = new Queue<Bone>(list);
         */
        RAnswer=new();
        UAnswer=new();
    }




    public override void Enter()
    {
        if (Question.Count > 0)
        {
            panel.gameObject.SetActive(false);
            auditP.gameObject.SetActive(true);
            resultP.gameObject.SetActive(false);

            Control.Instance.VisibilityOfPreparat(true);
            //slaveMashine.SetState<FAuditIntervue>();
            var a = Question.Dequeue();
            if (a.gameObject.name.StartsWith("R_") |
                a.gameObject.name.StartsWith("L_")
                )
            {
                RAnswer.Add(a.gameObject.name.Substring(2));
            }
            else
            {
                RAnswer.Add(a.gameObject.name);
            }
            auditP.CreateStep(a, this);
            Control.Instance.ChangePoint(a);
        }
        else {
            resultP.gameObject.SetActive(true);
            panel.gameObject.SetActive(false);
            auditP.gameObject.SetActive(false);
            Control.Instance.VisibilityOfPreparat(false);
            resultP.CreateResoult(RAnswer.ToArray(), UAnswer.ToArray());
            resultP.FillText(new string[] {
                ContentLoc.Instance.GetLocalText("repeat"),
                ContentLoc.Instance.GetLocalText("adsuescere") });
            resultP.SetIplementing(
                () => {
                    CaseCreation();
                    Enter();
                },
                () => {
                    rootMashine.SetState<FStudy>();
                }
                );



            //UnityEngine.Debug.Log($"{RAnswer.Count} - {UAnswer.Count}");

            
            

        }


    }

           


    public override void Exit()
    {
       panel.gameObject.SetActive(false);
       auditP.gameObject.SetActive(false);
       resultP .gameObject.SetActive(false);

    }

    public override void Update()
    {
        if (Question.Count==0) { 
            
        }
        
        //slaveMashine.Update();

    }

    public void MarkAnsver(string answ) { 
        UAnswer.Add(answ);
        Enter();
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
            //auditP.CreateStep();
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


