using System;

namespace MinLibrary.Dependencies
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class ProvideAttribute : Attribute
    {
    }
}