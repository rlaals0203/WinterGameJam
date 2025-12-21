using System;

namespace MinLibrary.Dependencies
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Method)]
    public class InjectAttribute : Attribute
    { }
}