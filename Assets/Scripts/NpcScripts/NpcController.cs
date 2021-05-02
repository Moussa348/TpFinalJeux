﻿using System.Collections;
using System.Collections.Generic;
using Script;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class NpcController : MonoBehaviour
{
    //Repasser pour voir ce qui n'ont pas besoin d'être private
    public float lookRadius = 50f;
    private NpcState _npcState;
    private Transform _target;
    private float _distance;
    private Animator _animator;
    private NavMeshAgent _agent;
    private List<Transform> _checkpoints;
    private readonly List<Command> _commands = new List<Command>() ;

    void Start()
    {
        //Mettre .transform
        _target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        _agent = GetComponent<NavMeshAgent>();
        _checkpoints = new List<Transform>(GameObject.Find("CheckPoints").GetComponentsInChildren<Transform>());
        _animator = GetComponent<Animator>();
        
        _commands.Add(new NavigationCommand(_agent, _checkpoints));
        _commands.Add(new DetectionCommand(transform,_target, _agent, lookRadius));
        _commands.Add(new AttackCommand(transform,_target,_agent,GetComponent<CharacterCombat>(),_animator));
        
        StartCoroutine(_commands[0].Execute());
    }

    void Update()
    {
         //TODO : if npc alive startCoroutine else foreach dans list<command> stopCoroutine
        StartCoroutine(_commands[1].Execute());
        StartCoroutine(_commands[2].Execute());
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,lookRadius);
    }
    
}

public enum NpcState
{
    Detecting,
    Navigating
}
