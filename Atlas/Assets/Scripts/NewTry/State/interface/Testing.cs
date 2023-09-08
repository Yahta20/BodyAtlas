
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Testing : IState
    {
    protected readonly scrollBeh sb;
    protected readonly Canvas pano;


    public Testing(Canvas canvas, scrollBeh s) {
        this.pano = canvas;
        this.pano.gameObject.SetActive(false);
        this.sb = s;
    }
    public void Enter()
    
    {
        this.pano.gameObject.SetActive(true);
        int numer = (int)Random.Range(0,Control.Instance.bones.Count);
        /*
         */
        
        sb.UpdateTesting();
        //sb.UpdateTesting();

    }
    public void Exit()
        {
        this.pano.gameObject.SetActive(false);

    }

        public void Update()
        {}
    }

