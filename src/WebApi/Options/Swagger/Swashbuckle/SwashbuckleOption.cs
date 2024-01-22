namespace WebApi.Options.Swagger.Swashbuckle;

internal sealed class SwashbuckleOption
{
    public DocOption Doc { get; set; } = new();

    public SecurityOption Security { get; set; } = new();

    internal sealed class DocOption
    {
        public string Name { get; set; }

        public InfoOption Info { get; set; } = new();

        internal sealed class InfoOption
        {
            public string Version { get; set; }

            public string Title { get; set; }

            public string Description { get; set; }

            public ContactOption Contact { get; set; } = new();

            public LicenseOption License { get; set; } = new();

            internal sealed class ContactOption
            {
                public string Name { get; set; }

                public string Email { get; set; }
            }

            internal sealed class LicenseOption
            {
                public string Name { get; set; }

                public string Url { get; set; }
            }
        }
    }

    internal sealed class SecurityOption
    {
        public DefinitionOption Definition { get; set; } = new();

        public RequirementOption Requirement { get; set; } = new();

        internal sealed class DefinitionOption
        {
            public string Name { get; set; }

            public SecuritySchemeOption SecurityScheme { get; set; } = new();

            internal sealed class SecuritySchemeOption
            {
                public string Description { get; set; }

                public string Name { get; set; }

                public int In { get; set; }

                public int Type { get; set; }

                public string Scheme { get; set; }
            }
        }

        internal sealed class RequirementOption
        {
            public OpenApiSecuritySchemeOption OpenApiSecurityScheme { get; set; } = new();

            public string[] Values { get; set; } = [];

            internal sealed class OpenApiSecuritySchemeOption
            {
                public ReferenceOption Reference { get; set; } = new();

                public string Scheme { get; set; }

                public string Name { get; set; }

                public int In { get; set; }

                internal sealed class ReferenceOption
                {
                    public int Type { get; set; }

                    public string Id { get; set; }
                }
            }
        }
    }
}
