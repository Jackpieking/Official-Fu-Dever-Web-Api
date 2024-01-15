namespace WebApi.Options.Swagger.Swashbuckle;

internal sealed class SwashbuckleOption
{
    internal DocOption Doc { get; set; } = new();

    internal SecurityOption Security { get; set; } = new();

    internal sealed class DocOption
    {
        internal string Name { get; set; }

        internal InfoOption Info { get; set; } = new();
    }

    internal sealed class InfoOption
    {
        internal string Version { get; set; }

        internal string Title { get; set; }

        internal string Description { get; set; }

        internal ContactOption Contact { get; set; } = new();

        internal LicenseOption License { get; set; } = new();

        internal sealed class ContactOption
        {
            internal string Name { get; set; }

            internal string Email { get; set; }
        }

        internal sealed class LicenseOption
        {
            internal string Name { get; set; }

            internal string Url { get; set; }
        }
    }

    internal sealed class SecurityOption
    {
        internal DefinitionOption Definition { get; set; } = new();

        internal RequirementOption Requirement { get; set; } = new();

        internal sealed class DefinitionOption
        {
            internal string Name { get; set; }

            internal SecuritySchemeOption SecurityScheme { get; set; } = new();

            internal sealed class SecuritySchemeOption
            {
                internal string Description { get; set; }

                internal string Name { get; set; }

                internal int In { get; set; }

                internal int Type { get; set; }

                internal string Scheme { get; set; }
            }
        }

        internal sealed class RequirementOption
        {
            internal OpenApiSecuritySchemeOption OpenApiSecurityScheme { get; set; } = new();

            internal string[] Values { get; set; }

            internal sealed class OpenApiSecuritySchemeOption
            {
                internal ReferenceOption Reference { get; set; } = new();

                internal string Scheme { get; set; }

                internal string Name { get; set; }

                internal int In { get; set; }

                internal sealed class ReferenceOption
                {
                    internal string Type { get; set; }

                    internal string Id { get; set; }
                }
            }
        }
    }
}
