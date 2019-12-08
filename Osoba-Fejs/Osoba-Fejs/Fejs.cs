using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Osoba_Fejs
{
    public class Fejs: IEnumerable<Osoba>
    {
        List<Osoba> listaKorisnika = new List<Osoba>();
        List<Osoba> izbaceniKorisnici = new List<Osoba>();

        public string imeFejsa;

        public Osoba dodaj(string ime_, string prezime_)
        {
            Osoba osoba = Osoba.kreiraj(ime_, prezime_, this);
            listaKorisnika.Add(osoba);
            return osoba;
        }

        public void izbaci(Osoba osoba_)
        {
            //proseci kroz sve Korisnike i makni ga svima iz prijatelja
            List<Osoba> dodatnoIzbaci = new List<Osoba>();
            foreach (var kori in listaKorisnika)
            {
                if (kori.prijatelji().Remove(osoba_))
                {
                    dodatnoIzbaci.Add(kori);
                }
            }
            Console.WriteLine("Izbacujem korisnika: " + osoba_.Ime + " " + osoba_.Prezime);
            listaKorisnika.Remove(osoba_);
            izbaceniKorisnici.Add(osoba_);
            foreach (var kori in dodatnoIzbaci)
            {
                izbaci(kori);
            }
        }

        //indeksiranje po prezimenu
        public OsobeIstogPrezimena this[string prezime_]
        {
            get
            {
                OsobeIstogPrezimena rez = new OsobeIstogPrezimena();

                foreach (var kori in listaKorisnika)
                {
                    if (kori.Prezime == prezime_)
                    {
                        rez.popisPrezime.Add(kori);
                    }
                }

                return rez;
            }
        }

        public void SortMoj()
        {
            //brPrijatelja-->prezime-->ime
            List<Osoba> sortiram = listaKorisnika;
            int brKorisnika = listaKorisnika.Count;

            for (int i = 0; i < brKorisnika - 1; i++)
            {
                for (int j = 0; j < brKorisnika - 1; j++)
                {

                    Osoba korisnik1 = listaKorisnika[j];
                    Osoba korisnik2 = listaKorisnika[j + 1];

                    //po broju prijatelja
                    if (korisnik1.brojPrijatelja() < korisnik2.brojPrijatelja())
                    {
                        //zamjena mjesta
                        Osoba temp = listaKorisnika[j + 1];
                        listaKorisnika[j + 1] = listaKorisnika[j];
                        listaKorisnika[j] = temp;
                        continue;
                    }

                    //po prezimenu
                    if (korisnik1.brojPrijatelja() == korisnik2.brojPrijatelja()
                        && string.Compare(korisnik1.Prezime, korisnik2.Prezime) == 1)
                    {
                        //zamjena mjesta
                        Osoba temp = listaKorisnika[j + 1];
                        listaKorisnika[j + 1] = listaKorisnika[j];
                        listaKorisnika[j] = temp;
                        continue;
                    }

                    //po imenu
                    if (korisnik1.brojPrijatelja() == korisnik2.brojPrijatelja()
                        && string.Compare(korisnik1.Prezime, korisnik2.Prezime) == 0
                        && string.Compare(korisnik1.Ime, korisnik2.Ime) == 1)
                    {
                        //zamjena mjesta
                        Osoba temp = listaKorisnika[j + 1];
                        listaKorisnika[j + 1] = listaKorisnika[j];
                        listaKorisnika[j] = temp;
                        continue;
                    }
                }
            }
        }

        public void Sort()
        {
            listaKorisnika.Sort();
            return;
        }

        public IEnumerator<Osoba> GetEnumerator()
        {
            foreach (Osoba x in listaKorisnika)
            {
                yield return x;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void izbacen(Osoba osoba_)
        {
            if (izbaceniKorisnici.IndexOf(osoba_) != -1)
            {
                throw new System.Exception("Pristup izbacenoj osobi " + osoba_.Ime + " " + osoba_.Prezime);
            }
        }

        public void ispisKorisnika()
        {
            Console.WriteLine("Korisnici fejsa: " + imeFejsa);
            foreach (var korisnik in listaKorisnika)
            {
                Console.WriteLine(korisnik.Prezime + " " + korisnik.Ime + " " + korisnik.brojPrijatelja());
            }
        }
    }

    
}
