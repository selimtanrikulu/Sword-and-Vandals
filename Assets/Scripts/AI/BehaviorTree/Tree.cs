using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTree
{
    public abstract class Tree : MonoBehaviour
    {
        protected AIController aiController;
        protected CharacterStateController characterStateController;
        private Node _root = null;
        protected virtual void Start()
        {
            aiController = GetComponent<AIController>();
            characterStateController = GetComponent<CharacterStateController>();
            _root = SetupTree();
        }

        private float timer = 0.0f;
        protected void Update()
        {
            timer += Time.deltaTime;
            if (timer >= 1.0f)
            {
                if (_root != null)
                    _root.Evaluate();
                timer = 0.0f;
            }

        }

        protected abstract Node SetupTree();
    }
    
}
