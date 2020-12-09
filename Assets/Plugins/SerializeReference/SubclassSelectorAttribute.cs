using System;
using UnityEngine;

[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
public class SubclassSelectorAttribute : PropertyAttribute
{
    public bool IsIncludeMono { get; }

    public SubclassSelectorAttribute(bool includeMono = false)
    {
        IsIncludeMono = includeMono;
    }
}