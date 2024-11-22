using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using System.Diagnostics;
using System.Text;

namespace AocHelper.Generator;

[Generator]
public sealed class ProblemLoaderGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var provider = context.SyntaxProvider.CreateSyntaxProvider(
            predicate: (x, ct) => x is ClassDeclarationSyntax,
            transform: (x, ct) => /*get the type namespace and name */
            {
                if (x.Node is ClassDeclarationSyntax tds)
                {
                    // check that class inherits from BaseProblem. todo, travel the type hierarchy
                    if (tds.BaseList is null || !tds.BaseList.Types.Any(x => x.Type.ToString() == "BaseProblem"))
                    {
                        return null;
                    }
                    //get the namespace
                    var parent = tds.Parent;
                    while (parent is not NamespaceDeclarationSyntax && parent is not FileScopedNamespaceDeclarationSyntax)
                    {
                        if(parent is null)
                        {
                            return null;
                        }
                        parent = parent?.Parent;
                    }
                    var ns = 
                        (parent as NamespaceDeclarationSyntax)?.Name.ToString()
                        ?? (parent as FileScopedNamespaceDeclarationSyntax)?.Name.ToString();

                    return $"{ns}.{tds.Identifier.Text}";


                }
                return null;
            })
            .Where(x => x is not null)
            .Collect();

        context.RegisterSourceOutput(provider, (spc, t) => spc
        .AddSource("ProblemLoader.g", CreateSource(t)));
    }

    private string CreateSource(IEnumerable<string> t)
    {
        var sb = new StringBuilder();

        sb.AppendLine("using System;");
        sb.AppendLine("using System.Collections.Generic;");
        sb.AppendLine("using System.Linq;");

        sb.AppendLine("namespace AoCHelper;");
        sb.AppendLine("public class ProblemLoader");
        sb.AppendLine("{");
        sb.AppendLine($"  public static (Type, Func<object?>)[] LoadAllProblems()");
        sb.AppendLine("  {");
        sb.AppendLine("    return new (Type, Func<object?>)[]");
        sb.AppendLine("    {");
        foreach (var type in t.Select((x, i) => (i, x)))
        {
            sb.Append($"      (typeof({type.x}), () => new {type.x}())");
            if (type.i != t.Count() - 1)
            {
                sb.AppendLine(",");
            }
            else
            {
                sb.AppendLine();
            }
        }
        sb.AppendLine("    };");
        sb.AppendLine("  }");
        sb.AppendLine("}");
        return sb.ToString();
    }
}