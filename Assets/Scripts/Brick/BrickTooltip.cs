using System;
using System.Collections.Generic;
using Scoring;
using TMPro;
using UI;
using UnityEngine;

namespace Brick
{
    public class BrickTooltip : MonoBehaviour
    {

        private ABrick _brick;
        private HealthManager _healthManager;
        private Camera _camera;

        private bool _toolTipOn;
        
        /// <summary>
        /// Current Text component of the tooltip
        /// </summary>
        private TextMeshProUGUI _currentText;

        private void Awake()
        {
            _brick = GetComponentInParent<ABrick>();
            _healthManager = GetComponentInParent<HealthManager>();
            _camera = Camera.main;
        }

        private void OnMouseEnter()
        {
            if(TooltipScreenSpaceUI.Instance == null) return;
            TooltipScreenSpaceUI.Instance.ShowTooltip(GenerateTooltipText);
        }

        private void OnMouseExit()
        {
            if(TooltipScreenSpaceUI.Instance == null) return;
            TooltipScreenSpaceUI.Instance.HideTooltip();
        }

        private void OnDisable()
        {
            if(TooltipScreenSpaceUI.Instance == null) return;
            TooltipScreenSpaceUI.Instance.HideTooltip();
        }

        /// <summary>
        /// Using info from the brick, generates the text to display in the tooltip
        /// </summary>
        /// <returns>A list of sentences representing the state of the brick</returns>
        private string GenerateTooltipText()
        {
            var text = new List<string>
            {
                "Type: " + GetBrickType(_brick),
                "Health: " + _healthManager.Health,
                _brick.CanLevelUp() ? _brick.GetUpgradeCost() + " points to upgrade" : "Max level reached",
                "Death reward: " + _brick.ScoreDestroyValue + " points" + " * " + ScoreManager.Instance.ScoreMultiplier + " = " + _brick.ScoreDestroyValue * ScoreManager.Instance.ScoreMultiplier + " points"
            };

            // Special cases
            switch (_brick)
            {
                case BallTrajModifierBrick ballTrajBrick:
                    //add cooldown if it's a boombox
                    if (ballTrajBrick.force < 0)
                    {
                        text.Add("Cooldown: " + ballTrajBrick.Cooldown + "s");
                    }
                    break;
                case PointBonusBrick pointBonusBrick:
                    text.Add("Current Multiplier: " + (1 + pointBonusBrick.BonusMultiplier));
                    break;
            }
            
            return string.Join("\n", text);
        }

        private string GetBrickType(IBrick brick)
        {
            switch (brick)
            {
                case BasicBrick _:
                    return "Basic";
                case GoldBrick _:
                    return "Gold";
                case BallTrajModifierBrick ballTraj:
                    return ballTraj.force > 0 ? "BoomBox" : "Magnet";
                case PointBonusBrick _:
                    return "Profit";
                case BuffBrick _:
                    return "Buff";
                case ReverseTrajectoryBrick _:
                    return "Reverter";
                default:
                    throw new ArgumentOutOfRangeException(nameof(brick));
            }

        }
    }
}