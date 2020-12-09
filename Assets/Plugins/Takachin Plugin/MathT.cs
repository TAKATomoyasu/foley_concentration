public class MathT
{
    // -180から+180の間で角度を返す
    public static float Get180Degrees(float num)
    {
        num += 180;
        num %= 360;
        if (num < 0) num += 180;
        else num -= 180;
        return num;
    }

    public static float Remap(float value, float inputMin, float inputMax, float outputMin, float outputMax)
    {
        return (value - inputMin) * ((outputMax - outputMin) / (inputMax - inputMin)) + outputMin;
    }
}