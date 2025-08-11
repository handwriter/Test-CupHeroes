using UniRx;
using UnityEngine;

namespace _Project.Runtime
{
    public class Team
    {
        public ReactiveProperty<int> AliveMembers = new ();
        private ITeamMember[] _members;
        private int _activeTargetIndex = 0;
        
        public Team() {}

        public Team(ITeamMember[] teamMembers)
        {
            SetupMembers(teamMembers);
        }

        public void SetupMembers(ITeamMember[] teamMembers)
        {
            AliveMembers.Value = teamMembers.Length;
            foreach (var member in teamMembers)
            {
                member.Health
                    .Where((value) => value == 0)
                    .Subscribe((_) => AliveMembers.Value--);
            }    
            _members = teamMembers;
        }

        public void Attack(Vector3 targetPosition)
        {
            foreach (var member in _members)
            {
                if (member.Health.Value > 0) member.Attack(targetPosition);
            }
        }

        public Vector3 GetTargetPosition()
        {
            int cycles = 0;
            while (!_members[_activeTargetIndex].GetObject())
            {
                _activeTargetIndex++;
                if (_activeTargetIndex >= _members.Length)
                {
                    _activeTargetIndex = 0;
                    cycles++;
                }
                if (cycles == 2) return Vector3.zero;
            }
            Vector3 targetPosition = _members[_activeTargetIndex].GetPosition();
            _activeTargetIndex++;
            if (_activeTargetIndex >= _members.Length) _activeTargetIndex = 0;
            return targetPosition;
        }
    }
}