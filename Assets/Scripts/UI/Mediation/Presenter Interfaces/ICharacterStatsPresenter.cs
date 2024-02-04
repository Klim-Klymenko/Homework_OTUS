﻿using System;
using System.Collections.Generic;

namespace PM
{
    public interface ICharacterStatsPresenter
    {
        event Action<ICharacterStatPresenter> OnStatAdded; 
        event Action<ICharacterStatPresenter> OnStatRemoved;
        
        IReadOnlyList<ICharacterStatPresenter> InitialStatPresenters { get; }
    }
}