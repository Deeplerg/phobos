using ExpressionCalculator.Abstractions.Tokenization.Tokens;

namespace Phobos.Core.UnknownVariable;

public record class UnknownVariableToken(string VariableName) : IToken;
