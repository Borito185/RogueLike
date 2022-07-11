using System;
using Assets.Code.Input;
using Assets.Code.Skills;
using Assets.Code.Skills.Active;
using Assets.Code.Stats;
using Assets.Code.Util;
using Mirror;
using UnityEngine;

namespace Assets.Code.Player
{
    [RequireComponent(typeof(Player))]
    public class PlayerController : NetworkBehaviour
    {
        private static InputManager Input => InputManager.Instance;
        private Player _player;
        [SerializeField]
        private Rigidbody2D _body;

        public override void OnStartClient()
        {
            _player = GetComponent<Player>();
            _body = GetComponent<Rigidbody2D>();

            if (!isLocalPlayer)
                return;

            Input.Direction.OnValueSet += Move;
            Input.Dash.OnValueSet += MovementSkill;
        }
        private void OnDestroy()
        {
            if (!isLocalPlayer)
                return;

            if (Input == null)
                return;
            Input.Direction.OnValueSet -= Move;
            Input.Dash.OnValueSet -= MovementSkill;
        }

        private void Move(object sender, EventValueEventArgs<Vector2> eventArgs)
        {
            Vector2 velocity = eventArgs.NewValue.ToIsometricVector();
            velocity *= _player.Stats.FindOfName(StatsEnum.Speed.ToString()).Value;
            _body.velocity = velocity;
        }
        private void MovementSkill(object sender, EventValueEventArgs<bool> eventArgs)
        {
            if (!eventArgs.FromDefaultToDefined)
                return;
            _player.UseSkill(0);
        }
    }
}
