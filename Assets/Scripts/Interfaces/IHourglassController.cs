using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHourglassController
{
    IHourglassState HourglassState { get; set; }
}
