using System.Text;
using ExpressionCalculator.Abstractions.Tokenization.Parsing;
using ExpressionCalculator.Tokenization;

namespace Phobos.Core.UnknownVariable;

[ParsingPriority(300_000)]
public class UnknownVariableParser : ITokenParser<UnknownVariableToken>
{
    public bool CanParse(StringSlice input)
    {
        char firstCharacter = input[0];
        return char.IsLetter(firstCharacter);
    }

    public TokenParsingResult<UnknownVariableToken> Parse(StringSlice currentInput)
    {
        var parseVariableResult = ParseVariableName(currentInput);

        int skippedCharacters = parseVariableResult.SkippedCharacters;
        string variableName = parseVariableResult.VariableName;

        var token = new UnknownVariableToken(variableName);
        var parsingResult = new TokenParsingResult<UnknownVariableToken>(token, skippedCharacters);
        return parsingResult;
    }

    private (int SkippedCharacters, string VariableName) ParseVariableName(StringSlice input)
    {
        var nameBuilder = new StringBuilder();

        bool encounteredLetter = false;
        int skippedCharacters = 0;

        foreach (var e in input)
        {
            if (!encounteredLetter)
            {
                if (char.IsLetter(e))
                {
                    encounteredLetter = true;
                }
                else
                {
                    throw new InvalidOperationException("Variable name must start with a letter.");
                }
            }

            if (char.IsLetterOrDigit(e))
            {
                nameBuilder.Append(e);
                skippedCharacters++;
            }
            else
            {
                break;
            }
        }

        return (skippedCharacters, nameBuilder.ToString());
    }
}
