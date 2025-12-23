using System;

namespace KimMin.Dependencies
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ProvideAttribute : Attribute
    {
    }
}