using UnityEngine;
using System.Collections;

public class EntityStateManager : OmonoPehaviour {
	EState entityState;
	AState attackState;
	Direction entityDirection;

	public void SetEntityState(EState stateToSet)
	{
		entityState = stateToSet;
	}

	public void SetAttackState(AState stateToSet)
	{
		attackState = stateToSet;
	}

	public void SetDirectionState(Direction stateToSet)
	{
		entityDirection = stateToSet;
	}

	public EState GetEntityState()
	{
		return entityState;
	}

	public AState GetAttackState()
	{
		return attackState;
	}

	public Direction GetDirState()
	{
		return entityDirection;
	}

	public bool Equals(EState stateToCheck)
	{
		return (entityState.Equals(stateToCheck));
	}

	public bool Equals(AState stateToCheck)
	{
		return (attackState.Equals(stateToCheck));
	}

	public bool Equals(Direction stateToCheck)
	{
		return (entityDirection.Equals(stateToCheck));
	}
}
