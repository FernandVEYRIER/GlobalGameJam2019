using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    public enum Player : int
    {
        P1 = 0,
        P2 = 1
    }

    private string[] buildInputs = new[] {"BuildP1", "BuildP2"};
    private string[] throwInputs = new[] { "ThrowP1", "ThrowP2" };

    public Player playerID;

    private int _playerID;
    private Animator _animator;

    private Vector3 _initialScale;
    // Start is called before the first frame update
    void Start()
    {
        _playerID = (int) playerID;
        _initialScale = transform.localScale;
        _animator = GetComponent<Animator>();
    }

    public void Flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1f, transform.localScale.y, transform.localScale.z);
    }

    public void ResetFlip()
    {
        transform.localScale = _initialScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown(throwInputs[_playerID]))
        {
            _animator.SetTrigger("Throw");
        }
        else if (Input.GetButtonDown(buildInputs[_playerID]))
        {
            _animator.SetTrigger("Build");
        }
    }
}
