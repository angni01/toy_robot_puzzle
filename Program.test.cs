using Xunit;

public class ToyRobotTest
{
    [Fact]
    public void PlaceRobot_ShouldReturnCorrectPosition()
    {
        var context = new SharedContext();
        var action = new Action(context);
        action.PlaceRobot("1,2,NORTH");
        Assert.Equal(1, context.currX);
        Assert.Equal(2, context.currY);
        Assert.Equal("NORTH", context.direction);
    }

    [Fact]
    public void ChangeDirection_ShouldReturnCorrectDirection()
    {
        string[] args = {"1","2","NORTH"};
        var context = new SharedContext(args);
        var action = new Action(context);
        action.ChangeDirection("LEFT");
        Assert.Equal("WEST", context.direction);
    }

    [Fact]
    public void MoveRobot_ShouldReturnCorrectPosition()
    {
        string[] args = {"1","2","NORTH"};
        var context = new SharedContext(args);
        var action = new Action(context);
        action.MoveRobot();
        Assert.Equal(1, context.currX);
        Assert.Equal(3, context.currY);
        Assert.Equal("NORTH", context.direction);
    }
}
