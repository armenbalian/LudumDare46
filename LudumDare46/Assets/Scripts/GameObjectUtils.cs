using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    static class GameObjectUtils
    {
        public static void SetActive(GameObject _gameObject, bool active)
        {
            var animator = _gameObject.GetComponent<Animator>();
            if (animator != null)
            {
                animator.enabled = active;
            }
            else
            {
                var animChild = _gameObject.GetComponentInChildren<Animator>();
                if (animChild != null)
                {
                    animChild.enabled = active;                    
                }
                else
                {
                    var animParent = _gameObject.GetComponentInParent<Animator>();
                    if (animParent != null)
                    {
                        animParent.enabled = active;
                    }
                }
            }

            var sprites = _gameObject.GetComponentsInChildren<SpriteRenderer>();
            foreach (var sprite in sprites)
            {
                sprite.enabled = active;
            }


            var meshrenderer = _gameObject.GetComponent<MeshRenderer>();
            if (meshrenderer != null)
            {
                meshrenderer.enabled = active;
            }

            var meshrenderers = _gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (var mesh in meshrenderers)
            {
                mesh.enabled = active;
            }
        }
    }
}
