using System;
using UnityEngine;

namespace Assets.Scripts.Data
{
    [Serializable]
    public class ScoreHandler
    {
        private float _scoreSinceLastCombo;
        private float _comboMultiplier = 1;

        [SerializeField]
        private AnimationCurve _comboCurve;

        public float Score { get; private set; }

        public float ComboMultiplier
        {
            get => _comboMultiplier;

            private set
            {
                if (_comboMultiplier != value)
                    OnComboChange?.Invoke(this, new ComboEvent { PreviousCombo = _comboMultiplier, CurrentCombo = value });
                _comboMultiplier = value;
            }
        }

        public event EventHandler<ComboEvent> OnComboChange;

        public void AddPoints(float amount)
        {
            if (amount <= 0)
            {
                _scoreSinceLastCombo = 0;
            }
            ComboMultiplier = _comboCurve.Evaluate(_scoreSinceLastCombo);
            Score += amount * ComboMultiplier;
            _scoreSinceLastCombo += amount * ComboMultiplier;
        }
    }

    public class ComboEvent : EventArgs
    {
        public float PreviousCombo;
        public float CurrentCombo;
    }
}