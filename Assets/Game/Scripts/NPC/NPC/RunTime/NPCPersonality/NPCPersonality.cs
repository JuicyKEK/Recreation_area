namespace Game.Scripts.NPC.RunTime.NPC
{
    public class NPCPersonality
    {
        public float StartLevelOfCourage => _startLevelOfCourage;
        public float StartLevelOfTrust => _startLevelOfTrust;
        public float SpeedMove => _speedMove;
        public float SpeedRun => _speedRun;
        public float SpeedAction => _speedAction;
        public float ArmLength => _armLength;
        public float MaxHP => _maxHP;
        
        private float _startLevelOfCourage = 0.5f;
        private float _startLevelOfTrust = 0.5f;
        private float _speedMove = 0.5f;
        private float _speedRun = 0.5f;
        private float _speedAction;
        private float _armLength;
        private float _maxHP;
        
        public NPCPersonality(float startLevelOfCourage, float startLevelOfTrust, float speedMove, float speedRun
            ,float speedAction = 1, float armLength = 1, float maxHP = 10f)
        {
            _startLevelOfCourage = startLevelOfCourage;
            _startLevelOfTrust = startLevelOfTrust;
            _speedMove = speedMove;
            _speedRun = speedRun;
            _speedAction = speedAction;
            _armLength = armLength;
            _maxHP = armLength;
        }
    }
}