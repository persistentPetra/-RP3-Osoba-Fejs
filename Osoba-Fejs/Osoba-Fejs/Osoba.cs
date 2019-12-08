using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osoba_Fejs
{
    public class Osoba : IComparable<Osoba>
    {
        string ime;
        string prezime;
        Fejs mreza;

        List<Osoba> listaPrijatelja = new List<Osoba>();

        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                ime = value;
            }
        }

        public string Prezime
        {
            get
            {
                return prezime;
            }

            set
            {
                prezime = value;
            }
        }

        public Fejs Mreza
        {
            get
            {
                return mreza;
            }

            set
            {
                mreza = value;
            }
        }

        private Osoba(string ime_, string prezime_, Fejs mreza_)
        {
            ime = ime_;
            prezime = prezime_;
            mreza = mreza_;
        }

        public static Osoba kreiraj(string ime_, string prezime_, Fejs mreza_)
        {
            Osoba os = new Osoba(ime_, prezime_, mreza_);
            return os;
        }

        public int brojPrijatelja()
        {
            return listaPrijatelja.Count;
        }

        public List<Osoba> prijatelji()
        {
            return listaPrijatelja;
        }

        public void ispisiPrijatelje()
        {
            Console.WriteLine("Prijatelji osobe " + prezime + " " + ime);
            foreach (var prija in listaPrijatelja)
            {
                Console.WriteLine(prija.prezime + " " + prija.ime);
            }
        }

        private void dodaj(Osoba osoba_)
        {
            listaPrijatelja.Add(osoba_);
        }

        public static Osoba operator +(Osoba x, Osoba y)
        {
            try
            {
                x.Mreza.izbacen(x);
                y.Mreza.izbacen(y);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return x;
            }

            //jesu li iz istoga Fejsa
            if (x.Mreza != y.Mreza)
            {
                Console.WriteLine("Korisnik " + x.Prezime + " " + x.ime + "i korisnik " + y.Prezime + " " + y.Ime + " su iz razlicitih su mreza");
                return x;
            }
            //jesu li vec prijatelji od ranije
            if (x.prijatelji().IndexOf(y) > -1)
            {
                Console.WriteLine("Korisnik " + x.ime + " " + x.prezime + " i " + y.ime + " " + y.prezime + " su vec prijatelji");
                return x;
            }

            x.dodaj(y);
            y.dodaj(x);
            Console.WriteLine("Korisnici " + x.ime + " " + x.prezime + " & " + y.ime + " " + y.prezime + " su se medusobno dodali");
            return x;
        }

        public static Osoba operator -(Osoba x, Osoba y)
        {
            try
            {
                x.Mreza.izbacen(x);
                y.Mreza.izbacen(y);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return x;
            }

            if (x.listaPrijatelja.Remove(y))
            {
                y.listaPrijatelja.Remove(x);
                //je li osoba y ostala bez prijatelja
                if (y.brojPrijatelja() == 0)
                {
                    y.Mreza.izbaci(y);
                }
                if (x.brojPrijatelja() == 0)
                {
                    x.Mreza.izbaci(x);
                }

            }
            return x;
        }

        private void meduPrijateljiRekurzija(List<Osoba> poveznicaPrijatelji_, Osoba prov, Osoba osoba_, List<Osoba> provjereni_, Osoba preskoci)
        {
            if (prov == preskoci) return;
            foreach (var kori in prov.prijatelji())
            {
                //Console.WriteLine("trazim korisnika "+ osoba_.Prezime + " od korisnika " + prov.Prezime + " gledam prijatelja " + kori.Prezime); 
                if (provjereni_.IndexOf(kori) > -1) continue;
                if (kori == osoba_)
                {
                    //ima li je vec medu prijateljima 
                    if (poveznicaPrijatelji_.IndexOf(prov) == -1)
                    {
                        poveznicaPrijatelji_.Add(prov);
                        //Console.WriteLine("ODGOVARA");
                        //ima li koljena izmedu
                        List<Osoba> provjereni = new List<Osoba>();
                        provjereni.Add(this);
                        foreach (var kori_ in listaPrijatelja)
                        {
                            if (kori_ == prov) continue;
                            provjereni.Add(kori_);
                            meduPrijateljiRekurzija(poveznicaPrijatelji_, kori_, prov, provjereni, preskoci);
                        }
                    }
                }
                else
                {
                    if (kori == preskoci) continue;
                    provjereni_.Add(kori);
                    meduPrijateljiRekurzija(poveznicaPrijatelji_, kori, osoba_, provjereni_, preskoci);
                }
            }
        }

        public List<Osoba> meduPrijatelji(Osoba osoba_)
        {
            try
            {
                this.Mreza.izbacen(this);
                osoba_.Mreza.izbacen(osoba_);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return new List<Osoba>();
            }

            List<Osoba> poveznicaPrijatelji = new List<Osoba>();
            List<Osoba> provjereni = new List<Osoba>();
            if (this == osoba_) return poveznicaPrijatelji;
            provjereni.Add(this);
            foreach (var kori in listaPrijatelja)
            {
                if (kori == osoba_) continue;
                provjereni.Add(kori);
                //Console.WriteLine("trazim korisnika "+ osoba_.Prezime + " -->Od korisnika " + this.Prezime + " gledam prijatelja " + kori.Prezime); 
                meduPrijateljiRekurzija(poveznicaPrijatelji, kori, osoba_, provjereni, osoba_);
            }
            return poveznicaPrijatelji;
        }

        public int CompareTo(Osoba osoba_)
        {
            try
            {
                this.Mreza.izbacen(this);
                osoba_.Mreza.izbacen(osoba_);
            }
            catch (System.Exception e)
            {
                Console.WriteLine(e.Message);
                return 0;
            }
            if (brojPrijatelja() < osoba_.brojPrijatelja()) return 1;

            if (brojPrijatelja() == osoba_.brojPrijatelja()
                && string.Compare(Prezime, osoba_.Prezime) == 1) return 1;

            if (brojPrijatelja() == osoba_.brojPrijatelja()
                && string.Compare(Prezime, osoba_.Prezime) == 0
                && string.Compare(Ime, osoba_.Ime) == 1) return 1;

            if (brojPrijatelja() == osoba_.brojPrijatelja()
               && string.Compare(Prezime, osoba_.Prezime) == 0
               && string.Compare(Ime, osoba_.Ime) == 0) return 0;

            return -1;
        }


        public void prezimenjaci()
        {
            Fejs f = this.Mreza;
            Console.WriteLine("PREZIMENJACI od " + Ime + " " + Prezime);
            foreach (var elem in f)
            {
                if (elem.Prezime == Prezime && this != elem && listaPrijatelja.IndexOf(elem) == -1)
                {
                    Console.WriteLine(elem.Ime + " " + elem.Prezime);
                }
            }
        }
    }
}
