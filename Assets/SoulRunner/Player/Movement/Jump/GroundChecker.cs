using System;
using UnityEngine;
using Zenject;

namespace SoulRunner.Player
{
  public class GroundChecker : MonoBehaviour
  {
    public Action OnGroundEnter;
    public Action OnGroundExit;

    public LayerMask Ground;
    [HideInInspector] public int Entity;
    
    private GroundCheckerService _groundCheckerSvc;

    [Inject]
    public void Construct(GroundCheckerService groundCheckerSvc)
    {
      _groundCheckerSvc = groundCheckerSvc;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      if ((collision.gameObject.layer & Ground) > 0)
        _groundCheckerSvc.OnGroundEnter(Entity);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      if ((collision.gameObject.layer & Ground) > 0) 
        OnGroundExit?.Invoke();
    }
  }
}