using System.Collections;
using UnityEngine;

namespace Game.Enemy.States
{
    [RequireComponent(typeof(Animator))]
    public class StateFrenzy : MonoBehaviour
    {
        Animator _anim;

        void Awake()
        {
            _anim = GetComponent<Animator>();
        }

        Coroutine _seekCoroutine;
        public void StartFrenzy()
        {
            _anim.enabled = false;
            _seekCoroutine = StartCoroutine(SeekLoop());
        }

        IEnumerator SeekLoop()
        {
            yield break;
        }
    }
}