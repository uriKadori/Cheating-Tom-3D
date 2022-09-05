using System;
using System.Collections;
using UnityEngine;

namespace GamePlay
{
    [RequireComponent(typeof(LineRenderer))]
    [RequireComponent(typeof(MeshCollider))]
    public class Vision : MonoBehaviour, IPausable
    {
        public event Action OnHit;

        [SerializeField] float duration;
        [SerializeField] LineRenderer visionLine;
        [SerializeField] MeshCollider meshCollider;
        [SerializeField] Vector3 endSize;
        [SerializeField] Vector3 endRotate;
        [SerializeField] float rotateThreshold;
        [SerializeField] float rotateAmount;

        private bool pause;
        public IEnumerator Render()
        {
            visionLine.SetPosition(1, Vector3.zero);
            yield return StartCoroutine(Watching());
        }

        public void Pause()
        {
            pause = true;
        }

        public void UnPause()
        {
            pause = false;
        }

        private IEnumerator Watching()
        {
            var startRotation = transform.rotation;
            yield return StartCoroutine(VisionGrow());
            BakeMeshVision();
            yield return StartCoroutine(VisionRotate());
            yield return StartCoroutine(VisionChunk());
            transform.rotation = startRotation;
        }

        private void BakeMeshVision()
        {
            if (meshCollider.sharedMesh != null)
                return;

            var mesh = new Mesh();
            visionLine.BakeMesh(mesh);
            meshCollider.sharedMesh = mesh;
        }
        private IEnumerator Pausing()
        {
            while (pause)
            {
                yield return null;
            }
        }

        private IEnumerator VisionGrow()
        {
            var time = 0f;
            while (Vector3.Distance(visionLine.GetPosition(1), endSize) > Mathf.Epsilon)
            {
                yield return StartCoroutine(Pausing());
                time += Time.deltaTime;
                visionLine.SetPosition(1, Vector3.Lerp(Vector3.zero, endSize, time / duration));
                yield return null;
            }
        }

        private IEnumerator VisionRotate()
        {
            var endRotateQuaternion = Quaternion.Euler(endRotate);
            var angle = Quaternion.Dot(transform.rotation, endRotateQuaternion);
            while (angle > rotateThreshold || angle < -rotateThreshold)
            {
                yield return StartCoroutine(Pausing());
                transform.Rotate(Vector3.forward, rotateAmount);
                angle = Quaternion.Dot(transform.rotation, endRotateQuaternion);
                yield return null;
            }
        }

        private IEnumerator VisionChunk()
        {
            var time = 0f;
            while (Vector3.Distance(visionLine.GetPosition(1), Vector3.zero) > Mathf.Epsilon)
            {
                yield return StartCoroutine(Pausing());
                time += Time.deltaTime;
                visionLine.SetPosition(1, Vector3.Lerp(visionLine.GetPosition(1), Vector3.zero, time / duration));
                yield return null;
            }
        }

        private void OnTriggerEnter(Collider collider)
        {
            OnHit?.Invoke();
        }
    }
}