using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public interface ILoadingOperation
{
    string Description {get; }

    Task Load(Action<float> onProgress);
}
