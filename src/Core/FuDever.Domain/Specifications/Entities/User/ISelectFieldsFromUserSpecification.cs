using FuDever.Domain.Specifications.Base;

namespace FuDever.Domain.Specifications.Entities.User;

/// <summary>
///     Represent select fields from the "Users" table specification.
/// </summary>
public interface ISelectFieldsFromUserSpecification : IBaseSpecification<Domain.Entities.User>
{
    ISelectFieldsFromUserSpecification Ver1();

    ISelectFieldsFromUserSpecification Ver2();

    ISelectFieldsFromUserSpecification Ver3();

    ISelectFieldsFromUserSpecification Ver4();
}
