using System;
using System.Collections.Generic;
using Jypeli;
using Jypeli.Assets;
using Jypeli.Controls;
using Jypeli.Widgets;

public class FysiikkaPeli1 : PhysicsGame
{
    public override void Begin()
    {
        Camera.ZoomToLevel();
        Level.Background.Color = Color.Black;

        PhysicsObject p1 = new PhysicsObject(2 * 100.0, 2 * 100.0, Shape.Circle);
        p1.Y = Level.Bottom + 200.0;
        Add(p1);

        PhysicsObject p2 = new PhysicsObject(2 * 50.0, 2 * 50.0, Shape.Circle);
        p2.Y = p1.Y + 100 + 50;
        Add(p2);

        PhysicsObject p3 = new PhysicsObject(2 * 30.0, 2 * 30.0, Shape.Circle);
        p3.Y = p2.Y + 50 + 30;
        Add(p3);

        PhysicsStructure ukko = new PhysicsStructure(p1, p2, p3);
        ukko.Softness = 10;
        Add(ukko);

        Gravity = new Vector(0, -400);
        Level.CreateBorders();
        p3.Hit(new Vector(80, 1));

        Timer t = new Timer();
        t.Interval = 5.0;
        t.Timeout += VaihdaPainovoimaa;
        t.Start();

        Keyboard.Listen(Key.Escape, ButtonState.Pressed, Exit, "Poistu");
        Keyboard.Listen(Key.F1, ButtonState.Pressed, ShowControlHelp, "Näytä näppäinohjeet");
        Keyboard.Listen(Key.Up, ButtonState.Pressed, LyoPalloa, "Lyö keskipalloa ylöspäin", p2, new Vector(0, 1000));
        Keyboard.Listen(Key.Down, ButtonState.Pressed, LyoPalloa, "Lyö keskipalloa alaspäin", p2, new Vector(0, -1000));
        Keyboard.Listen(Key.Left, ButtonState.Pressed, LyoPalloa, "Lyö keskipalloa vasemmalle", p2, new Vector(-1000, 0));
        Keyboard.Listen(Key.Right, ButtonState.Pressed, LyoPalloa, "Lyö keskipalloa oikealle", p2, new Vector(1000, 0));

        Keyboard.Listen(Key.W, ButtonState.Pressed, LyoPalloa, "Lyö alapalloa ylöspäin", p1, new Vector(0, 1000));
        Keyboard.Listen(Key.S, ButtonState.Pressed, LyoPalloa, "Lyö alapalloa alaspäin", p1, new Vector(0, -1000));
        Keyboard.Listen(Key.A, ButtonState.Pressed, LyoPalloa, "Lyö alapalloa vasemmalle", p1, new Vector(-1000, 0));
        Keyboard.Listen(Key.D, ButtonState.Pressed, LyoPalloa, "Lyö alapalloa oikealle", p1, new Vector(1000, 0));
    }

    private void VaihdaPainovoimaa()
    {
        Gravity = Vector.FromLengthAndAngle(Gravity.Magnitude, Gravity.Angle + Angle.FromDegrees(90.0));
    }

    private void LyoPalloa(PhysicsObject pallo,Vector suunta)
    {
        pallo.Hit(suunta);
    }
}
