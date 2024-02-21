using Domain.Specifications.Base;

namespace Domain.Specifications.Entities.Position;

/// <summary>
///     Represent select fields from the "Positions" table specification.
/// </summary>
public interface ISelectFieldsFromPositionSpecification : IBaseSpecification<Domain.Entities.Position>
{
    ISelectFieldsFromPositionSpecification Ver1();

    ISelectFieldsFromPositionSpecification Ver2();

    ISelectFieldsFromPositionSpecification Ver3();
}
