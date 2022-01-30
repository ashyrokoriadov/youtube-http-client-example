namespace HttpClientExample.Library
{
    public class CalculatorRequest
    {
        public CalculatorRequest(double a, double b)
        {
            A = a;
            B = b;
        }

        public CalculatorRequest()
        {}

        public double A { get; set; }

        public double B { get; set; }
    }
}
