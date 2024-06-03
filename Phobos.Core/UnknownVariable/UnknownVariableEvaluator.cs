using ExpressionCalculator.Abstractions.Evaluation;
using ExpressionCalculator.Abstractions.Tokenization.Tokens;
using ExpressionCalculator.Tokenization.Tokens;

namespace Phobos.Core.UnknownVariable;

public class UnknownVariableEvaluator : ITokenEvaluator<UnknownVariableToken>
{
    public double Evaluate(IEnumerable<IToken> tokens)
    {
        throw new InvalidOperationException(
            $"{nameof(UnknownVariableToken)} must be replaced with a " +
            $"{nameof(NumberToken)} prior to calculating the value of the expression");
    }
}
