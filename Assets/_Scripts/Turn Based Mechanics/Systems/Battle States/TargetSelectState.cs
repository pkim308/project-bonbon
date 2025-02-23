using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class BattleStateMachine {
    public class TargetSelectState : BattleState {
        private SelectorManager _selectManager = new SelectorManager();
        private int _nextSelectedActor = 0; // Enemy Actor Selection

        public override void Enter(BattleStateInput i) {
            base.Enter(i);
            Debug.Log("Entering target select state");
            if (Input.ActiveActor() is EnemyActor) {
                // Enemy Actor Selection; pls fix thank you
                Input.SetActiveSkill(new SkillAction(Input.ActiveActor().data.SkillList()[0], MySM.actorList[_nextSelectedActor]));
                _nextSelectedActor = (_nextSelectedActor == 0) ? MySM.actorList.Count - 1 : 0;
                MySM.Transition<AnimateState>();
            }
            MySM.OnStateTransition.Invoke(this, Input);
        }
        
        public override void Update() {
            base.Update();
            Actor actor = _selectManager.CheckForSelect();
            if (actor != null) {
                Input.SetActiveSkill(new SkillAction(Input.ActiveSkill().Data(), actor));
                MySM.Transition<AnimateState>();
            }
        }

        public override void Exit(BattleStateInput input) {
            base.Exit(input);
        }
    }
}
