using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _moveClip, _loseClip, _pointClip;

    [SerializeField] private GameplayManager _gm;
    [SerializeField] private GameObject _explosionPrefab, _scoreParticlePrefab;

    private bool canClick;

    private void Start()
    {
        canClick = false;
    }

    private void Update()
    {
        if(canClick && Input.GetMouseButton(0))
        {
            SoungManager.instance.PlaySound(_moveClip);
            StartCoroutine(ChangeRadius());
        }
    }

    [SerializeField] private float _rotateSpeed;
    [SerializeField] private Transform _rotateTranform;

    private void FixedUpdate()
    {
        transform.localPosition = Vector3.up * currentRotateRadius;
        float rotateValue = _rotateSpeed * Time.fixedDeltaTime;
        rotateValue = (_startRotateRadius / currentRotateRadius);
        _rotateTranform.Rotate(0,0,rotateValue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            SoungManager.instance.PlaySound(_loseClip);
            _gm.GameEnded();
            return;
        }

        if(collision.CompareTag("Score"))
        {
            Destroy(Instantiate(_scoreParticlePrefab, transform.position, Quaternion.identity),2f);
            SoungManager.instance.PlaySound(_pointClip);
            _gm.UpdateScore();
            collision.gameObject.GetComponent<Score>().ScoreAdded();
            return;
        }
    }


    [SerializeField] private float _startRotateRadius;
    [SerializeField] private float _moveTime;

    [SerializeField] private List<float> _rotateRadius;
    private float currentRotateRadius;
    private int level;

    private IEnumerator ChangeRadius()
    {
        canClick = false;

        float moveStartRadius = _rotateRadius[level];
        float moveEndRadius = _rotateRadius[(level + 1) % _rotateRadius.Count];
        float moveOffset = moveEndRadius - moveStartRadius;
        float speed = 1 / _moveTime;
        float timeElapsed = 0f;

        while(timeElapsed < 1f)
        {
            timeElapsed += speed * Time.deltaTime;
            currentRotateRadius = moveStartRadius + moveOffset * timeElapsed;
            yield return new WaitForFixedUpdate();

        }

        canClick = true;
        level = (level + 1) % _rotateRadius.Count;
        currentRotateRadius = _rotateRadius[level];
    }
}
