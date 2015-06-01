using System;
using System.Numerics;

namespace zeroknowledge
{
    class Program
    {
        static Random random = new Random();
        static void Main(string[] args)
        {
            FiatShamir fs = new FiatShamir();

            Console.WriteLine(" ----------- Schlüsselerzeugungsphase ----------- ");
            Console.WriteLine("Alice erzeugt zwei Primzahlen p und q und ihr Produkt n = p * q");
            Console.WriteLine("n: {0} (öffentlich)", fs.n);
            Console.WriteLine("p: {0} (geheim)", fs.p);
            Console.WriteLine("q: {0} (geheim)", fs.q);

            Console.WriteLine("Alice wählt s (s<n und ggt(s,n)=1) und bildet v = s^2 mod n");
            BigInteger s = fs.GetCoPrime(fs.n);
            Console.WriteLine("s: {0}  (Geheimnis von Alice)", s);
            BigInteger v = (s * s) % fs.n;
            Console.WriteLine("v: {0}  (öffentlich)", v);



            Console.WriteLine(" ---------------- Anwendungsphase ---------------- ");
            Console.WriteLine("Alice wählt zufällig r (r<n und ggt(r,n)=1) und bildet x = r^2 mod n");
            BigInteger r = fs.GetCoPrime(fs.n);
            Console.WriteLine("r: {0}  (Geheimnis von Alice für diese Runde)", s);
            BigInteger x = (r * r) % fs.n;
            Console.WriteLine("x: {0} (öffentlich)", x);

            Console.WriteLine("Alice sendet x an Bob");
            Console.WriteLine();


            Console.WriteLine("Bob wählt zufällig ein Bit b aus und sendet b an Alice");
            int b = random.Next(0, 2);
            Console.WriteLine("b: {0}", b);

            BigInteger y = 0;
            if (b == 1) y = (r * s) % fs.n;
            if (b == 0) y = r % fs.n;
            Console.WriteLine("Alice sendet y");
            Console.WriteLine("y: {0} ", y);
            Console.WriteLine("Bob prüft");
            BigInteger yn = (y * y) % fs.n;

            BigInteger test = 0;
            if (b == 1) test = (x * v) % fs.n;
            if (b == 0) test = x % fs.n;

            if (yn == test)
            {
                Console.WriteLine("Auth OK");
            }
            else
            {
                Console.WriteLine("Auth Wrong");
            }
            Console.ReadLine();
        }

    }
}
