﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Blocks;
using Assets.Scripts.Game;
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
    public Spawner spawner;

    public float kickScale = 10f;

    public float kickSpeed = 10f;

    public Transform targetKick;

    private BlockBuilder _blockBuilder;
    //public 

    private int _playerID;
    private Animator _animator;

    private Vector3 _initialScale;

    private ABlock _block;

    private bool _isKicking;

    private Vector3 _finalScale;
    // Start is called before the first frame update
    void Start()
    {
        _playerID = (int) playerID;
        _initialScale = transform.localScale;
        _animator = GetComponent<Animator>();
        _finalScale = _initialScale * kickScale;
        _blockBuilder = GameManager.Instance.BlockBuilder;
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
        if (_isKicking == false)
        {
            if (Input.GetButtonDown(throwInputs[_playerID]))
            {
                _block = spawner.TakeBlock();
                _animator.SetTrigger("Throw");
            }
            else if (Input.GetButtonDown(buildInputs[_playerID]))
            {
                _block = spawner.TakeBlock();
                _animator.SetTrigger("Build");
            }
            if (_block)
            {

                var point = spawner.GetLastPoint();

                _block.transform.position = Vector3.MoveTowards(_block.transform.position, point,
                    spawner.blockSpeed * Time.deltaTime * 2);
            }
        }
        else
        {
            if (_block)
            {
                _block.transform.position = Vector3.MoveTowards(_block.transform.position, targetKick.position,
                    kickSpeed * Time.deltaTime);
                _block.transform.localScale = Vector3.Slerp(_block.transform.localScale, _finalScale, kickSpeed * Time.deltaTime);
            }
        }
    }

    public void Kick()
    {
        _isKicking = true;
    }

    public void EndKick()
    {
        _isKicking = false;
        if (playerID == Player.P1)
        {
            _blockBuilder.PushBlockLeft(_block);
        }
        else
        {
            _blockBuilder.PushBlockRight(_block);
        }
        _block = null;
    }
}