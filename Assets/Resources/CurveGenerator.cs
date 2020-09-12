public class CurveGenerator
{
    public static int[] GetCurve(int height, int length, int difference)
    {
        int minimum = height - difference,
            maximum = height + difference;

        System.Random random = new System.Random();

        int[] curve = new int[length];
        curve[0] = random.Next(minimum, maximum);

        for (int i = 1; i < length; ++i)
        {
            // I calculate the probabilities of events of the rise and fall of the curve,
            //     depending on the height and the previous value
            // First, find the percentage of the difference between the previous value and the normal (i.e height)
            double deviationPercent = (curve[i - 1] - height) / difference;
            // Growth probability is
            double growthPercent = 31 - (31 * deviationPercent);
            // Fall probability is 62 - growthPercent
            // Probability that the function will not change is 31
            // Double growth probability is
            double doubleGrowthPercent = 3.5 - (3.5 * deviationPercent);
            // Double fall probability is 7 - doubleGrowthPercent

            // Probabilities borders for convenient selection
            int borderGrowth = (int)growthPercent;
            int borderFall = 62;
            int borderNothing = borderFall + 31;
            int borderDoubleGrowth = borderNothing + (int)doubleGrowthPercent;
            // Border of double fall is 100

            int randomPercent = random.Next(1, 100);

            int change = 2;
            if (randomPercent <= borderGrowth)
                change = 1;
            else if (randomPercent <= borderFall)
                change = -1;
            else if (randomPercent <= borderNothing)
                change = 0;
            else if (randomPercent <= borderDoubleGrowth)
                change = -2;

            curve[i] = curve[i - 1] + change;

            if (curve[i] < minimum)
                curve[i] = minimum;
            if (curve[i] > maximum)
                curve[i] = maximum;
        }

        return curve;
    }
}
