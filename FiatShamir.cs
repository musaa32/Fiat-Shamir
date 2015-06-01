using System;
using System.Numerics;

namespace zeroknowledge
{
    public class FiatShamir
    {
        private Random Randomizer;

        private BigInteger Secret_P;

        public BigInteger p
        {
            get { return Secret_P; }
        }

        private BigInteger Secret_Q;

        public BigInteger q
        {
            get { return Secret_Q; }
        }
        private BigInteger Public_N;

        public BigInteger n
        {
            get { return Public_N; }
        }


        public FiatShamir()
        {
            Randomizer = new Random();
            this.Secret_P = this.GeneratePrime();
            this.Secret_Q = this.GeneratePrime();
            this.Public_N = BigInteger.Multiply(this.Secret_P, this.Secret_Q);
        }

        private BigInteger GeneratePrime()
        {
            BigInteger prime = 1;
            while (!this.IsPrime(prime)) { prime = RandomNumber(); }
            return prime;
        }

        private BigInteger RandomNumber()
        {
            byte[] buffer = new byte[sizeof(uint)];
            Randomizer.NextBytes(buffer);
            BigInteger random = new BigInteger(buffer);
            if (random < 0) random *= -1;
            return random;
        }



        private bool IsPrime(BigInteger number)
        {
            if (number == 1) return false;
            if (number == 2) return true;
            BigInteger boundary = Sqrt(number);
            for (BigInteger i = 2; i <= boundary; ++i)
            {
                if (number % i == 0) return false;
            }
            return true;
        }

        private BigInteger Sqrt(BigInteger n)
        {
            if (n == 0) return 0;
            if (n > 0)
            {
                int bitLength = Convert.ToInt32(Math.Ceiling(BigInteger.Log(n, 2)));
                BigInteger root = BigInteger.One << (bitLength / 2);

                while (!IsSqrt(n, root))
                {
                    root += n / root;
                    root /= 2;
                }

                return root;
            }

            throw new ArithmeticException("NaN");
        }

        private bool IsSqrt(BigInteger n, BigInteger root)
        {
            BigInteger lowerBound = root * root;
            BigInteger upperBound = (root + 1) * (root + 1);
            return (n >= lowerBound && n < upperBound);
        }


        public BigInteger GetCoPrime(BigInteger n)
        {
            BigInteger coprime;
            do
            {
                coprime = RandomNumber();
            }
            while ((coprime > 1 && coprime < n) && GreatestCommonDivisor(n, coprime) != 1);
            return coprime;
        }


        private BigInteger GreatestCommonDivisor(BigInteger a, BigInteger b)
        {
            BigInteger c;
            while (a != 0)
            {
                c = a;
                a = b % a;
                b = c;
            }
            return b;

        }

    }
}
