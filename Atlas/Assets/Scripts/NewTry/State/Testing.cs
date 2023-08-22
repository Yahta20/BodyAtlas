
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Testing : IState
    {
    protected readonly scrollBeh sb;
        public Testing(scrollBeh s) {
            sb = s;
        }
        
    
    
    
        public void Enter()
        {
        int numer = (int)Random.Range(0,Control.Instance.bones.Count);

           sb.UpdateTesting();
            
        }

        public void Exit()
        {}

        public void Update()
        {}
    }

