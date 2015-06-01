# Fiat-Shamir

* Fiat-Shamir-Algorithmus Implentierung in C#
* Fiat-Shamir implementation in C#




## Schlüsselerzeugungsphase:
* Alice erzeugt zwei Primzahlen p und q und ihr Produkt n=pq
* n : öffentlich
* p und q geheim


* Alice wählt s (s<n und ggt(s,n)=1) und bildet v = s^2 mod n
* v : öffentlich, s : individuelle Geheimnis von Alice

## Anwendungsphase(wird x-mal wiederholt):
Alice wählt zufällig r (r<n und ggt(r,n)=1) und bildet x = r^2 mod n
