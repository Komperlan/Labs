using Itmo.ObjectOrientedProgramming.Lab1.Constructions;
using Itmo.ObjectOrientedProgramming.Lab1.Simulations;
using Itmo.ObjectOrientedProgramming.Lab1.Trains;
using Itmo.ObjectOrientedProgramming.Lab1.UnitsOfMeasurement;

using Xunit;

namespace Lab1.Tests;

public class PathsTest
{
    [Fact]
    public void Simulation_ShouldReturnTrue_WhenTrainSpeedHigherConstructionSpeed()
    {
        var route = new Route([new PowerPath(new Meter(12), 2), new JustPath(new Meter(12))], new MeterPerSecond(1000));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.True(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnFalse_WhenOnePowerPathAndOneJustPathPassed()
    {
        var route = new Route([new PowerPath(new Meter(12), 2), new JustPath(new Meter(12))], new MeterPerSecond(1));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.False(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnTrue_WhenTrainSpeedLowerConstructionSpeedAndTrainTroughStation()
    {
        var route = new Route([new PowerPath(new Meter(12), 2), new JustPath(new Meter(12)), new Station(new MeterPerSecond(1000)), new JustPath(new Meter(12))], new MeterPerSecond(1000));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.True(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnFalse_WhenTrainSpeedHigherStationSpeed()
    {
        var route = new Route([new PowerPath(new Meter(12), 2), new Station(new MeterPerSecond(4)), new JustPath(new Meter(12))], new MeterPerSecond(1000));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.False(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnFalse_WhenTrainSpeedHigherPathSpeed()
    {
        var route = new Route([new PowerPath(new Meter(12), 2), new JustPath(new Meter(12)), new Station(new MeterPerSecond(1000)), new JustPath(new Meter(12))], new MeterPerSecond(4));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.False(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnTrue_WhenTrainSpeedLowerStationSpeedAndPathMaxSpeed()
    {
        var route = new Route(
            [new PowerPath(new Meter(12), 2), new JustPath(new Meter(12)), new PowerPath(new Meter(4), -2),
            new Station(new MeterPerSecond(4)), new JustPath(new Meter(12)), new PowerPath(new Meter(6), 2), new JustPath(new Meter(12)), new PowerPath(new Meter(4), -2)],
            new MeterPerSecond(4));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.True(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnFalse_WhenJustPath()
    {
        var route = new Route(
            [new JustPath(new Meter(12))], new MeterPerSecond(4));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.False(simulation.Result.Succes);
    }

    [Fact]
    public void Simulation_ShouldReturnFalse_WhenPowerPathWithDoublePowerForSlow()
    {
        var route = new Route(
            [new PowerPath(new Meter(12), 2), new PowerPath(new Meter(12), -2)], new MeterPerSecond(4));

        var train = new Train(1, 10, 1);

        var simulation = new Simulation(route, train);

        simulation.Simulate();

        Assert.False(simulation.Result.Succes);
    }
}