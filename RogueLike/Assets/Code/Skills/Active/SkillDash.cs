using System;
using System.Collections;
using Assets.Code.Input;
using Assets.Code.Player;
using Assets.Code.Stats;
using Assets.Code.Util;
using Mirror;
using UnityEngine;

namespace Assets.Code.Skills.Active
{
    [Serializable]
    [CreateAssetMenu(menuName = "Skills/Dash")]
    public class SkillDash : ActiveSkillBase
    {
        [SerializeField]
        private float _dashDistance = 3.0f;

        [SerializeField]
        private float _dashTime = 0.3f;

        protected override void Ability(Status user)
        {
            user.StartCoroutine(AbilityAsEnumerator(user));
        }

        private IEnumerator AbilityAsEnumerator(Status user)
        {
            if (!user.TryGetComponent(out Rigidbody2D rigidbody))
            {
                Debug.LogWarning($"{nameof(SkillDash)} couldn't find {nameof(Rigidbody2D)} to use");
                yield break;
            }
            Vector2 direction = InputManager.Instance.Direction.Value.ToIsometricVector();
            Vector2 directionAndDistance = direction * _dashDistance;

            float distanceLeft = directionAndDistance.magnitude;
            yield return new WaitForFixedUpdate();


            if (_dashTime <= 0)
            {
                rigidbody.MovePosition(rigidbody.position + directionAndDistance);
                yield break;
            }
            Vector2 velocity = directionAndDistance / _dashTime;

            do
            {
                Vector2 move = velocity * Time.fixedDeltaTime;
                float distance = move.magnitude;
                if (distance >= distanceLeft)
                {
                    move = direction * distanceLeft;
                    distanceLeft = -1f;
                }
                distanceLeft -= distance;

                rigidbody.MovePosition(rigidbody.position + move);
                yield return new WaitForFixedUpdate();
            } while (distanceLeft >= 0f);
        }
    }
}
