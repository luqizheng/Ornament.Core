using System;

namespace Ornament.Domain.Entities
{
    /// <summary>
    ///     Facilitates indicating which property(s) describe the unique signature of an
    ///     entity. See Entity.GetTypeSpecificSignatureProperties() for when this is leveraged.
    /// </summary>
    /// <remarks>
    ///     This is intended for use with <see cref="Entity" />. It may NOT be used on a <see cref="ValueObject" />.
    /// </remarks>
#if DNX451

    [Serializable]
#endif
    [AttributeUsage(AttributeTargets.Property)]
    internal class DomainSignatureAttribute : Attribute
    {
    }
}