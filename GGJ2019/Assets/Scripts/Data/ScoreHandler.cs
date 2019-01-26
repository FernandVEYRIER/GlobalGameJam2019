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
            //if (amount <= 0)
            //{
            //    _scoreSinceLastCombo = 0;
            //}
            Score += amount * ComboMultiplier;
            //_scoreSinceLastCombo += amount * ComboMultiplier;
        }

        /// <summary>
        /// Sets if the player managed to do a positive action, or failed. Upgrades the combo accordingly.
        /// </summary>
        /// <param name="isPositive"></param>
        public void SetPositiveAction(bool isPositive)
        {
            if (isPositive)
                _scoreSinceLastCombo += 1;
            else
                _scoreSinceLastCombo = 0;
            ComboMultiplier = _comboCurve.Evaluate(_scoreSinceLastCombo);
        }

        internal void AddPoints(object p)
        {
            throw new NotImplementedException();
        }
    }

    public class ComboEvent : EventArgs
    {
        public float PreviousCombo;
        public float CurrentCombo;
    }
}