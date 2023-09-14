﻿using System.Collections.Generic;

namespace Strom.Btmvvm
{
public abstract class AbstractVmVariable : IVmVariable
{
	private readonly List<IVmVariable> _viewModelVariables = new();

	protected AbstractVmVariable()
	{
		foreach (var fieldInfo in GetType().GetFields())
		{
			if (fieldInfo.GetValue(this) is IVmVariable variable)
			{
				_viewModelVariables.Add(variable);
			}
		}
	}

	public void Dispose()
	{
		foreach (var variable in _viewModelVariables)
		{
			variable.Dispose();
		}
	}

	public void Unbind()
	{
		foreach (var variable in _viewModelVariables)
		{
			variable.Unbind();
		}
	}
}
}