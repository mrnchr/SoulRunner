using System;
using SoulRunner.Player.ActionMachines;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class GroundChecker : MonoBehaviour
  {
    public Action OnGroundEnter;
    public Action OnGroundExit;

    public LayerMask Ground;
    [HideInInspector] public View View;
    
    [SerializeField] private ActionMachine _machine;  
    private GroundCheckerService _groundCheckerSvc;

    [Inject]
    public void Construct(GroundCheckerService groundCheckerSvc)
    {
      _groundCheckerSvc = groundCheckerSvc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if ((collision.gameObject.layer & Ground) > 0)
      {
        // _machine.IsOnGround = true;        
        _groundCheckerSvc.OnGroundEnter(View.Entity);
      }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      if ((collision.gameObject.layer & Ground) > 0)
      {
        OnGroundExit?.Invoke();
        _groundCheckerSvc.OnGroundExit(View.Entity);
      }
    }
  }
}