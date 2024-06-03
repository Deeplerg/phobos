using ExpressionCalculator;
using Phobos.Core.Drawing;
using Phobos.Core.Drawing.Components;
using Phobos.Core.Drawing.Pipeline;
using Phobos.Core.UnknownVariable;

namespace Phobos.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDrawingComponents(this IServiceCollection services)
    {
        services.AddTransient<IDrawingComponent<DrawingOptions>, AxesComponent>();
        services.AddTransient<IDrawingComponent<DrawingOptions>, FunctionsComponent>();
        services.AddTransient<IDrawingComponent<DrawingOptions>, LabelsComponent>();
        services.AddTransient<IDrawingComponent<DrawingOptions>, TicksComponent>();

        services.AddTransient<AxesComponent>();
        services.AddTransient<FunctionsComponent>();
        services.AddTransient<LabelsComponent>();
        services.AddTransient<TicksComponent>();

        return services;
    }

    public static IServiceCollection AddDrawingServices(this IServiceCollection services)
    {
        services.AddDrawingComponents();

        services.AddTransient<IDrawingPipeline<DrawingOptions>, DrawingPipeline>();
        services.AddTransient<IPipelineActionsCompositor, PipelineActionsCompositor>();

        services.AddTransient<GraphCanvas>();

        return services;
    }

    public static IServiceCollection AddExpressionCalculator(this IServiceCollection services)
    {
        services.AddTransient<ExpressionCalculator.ExpressionCalculator>(_ =>
        {
            var calculatorBuilder = ExpressionCalculatorBuilder.Default;
            calculatorBuilder.AddTokenParser<UnknownVariableParser, UnknownVariableToken>();
            calculatorBuilder.AddTokenEvaluator<UnknownVariableEvaluator, UnknownVariableToken>();

            return calculatorBuilder.Build();
        });

        return services;
    }
}