using System;
using System.Collections.Generic;

namespace Osoba_Fejs
{
    //kad smo dobili skup osoba iz iste mreze sa istim prezimenom
    //tada na njih dijeluje indekser sa imenima 
    public class OsobeIstogPrezimena
    {
        public List<Osoba> popisPrezime = new List<Osoba>();

        public List<Osoba> this[string ime_]
        {
            get
            {
                List<Osoba> rez = new List<Osoba>();

                foreach (var kori in popisPrezime)
                {
                    if (kori.Ime == ime_)
                    {
                        rez.Add(kori);
                    }
                }

                return rez;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Fejs f1 = new Fejs();
            f1.imeFejsa = "f1";

            Osoba o1 = f1.dodaj("a", "aa");
            Osoba o2 = f1.dodaj("b", "bb");
            Osoba o3 = f1.dodaj("c", "aa");
            Osoba o4 = f1.dodaj("d", "aa");
            Osoba o5 = f1.dodaj("f", "aa");

            o3 += o4;
            o3.prezimenjaci();

            f1.ispisKorisnika();

            o1+=o2; 
            o1+=o3; 
            o1.ispisiPrijatelje(); 

            o2+=o1; 
            o2+=o1; // ne doda ga jer su vec prijatelji
            o2.ispisiPrijatelje(); 

            Fejs f2 = new Fejs(); 
            f2.imeFejsa = "f2"; 
            Osoba osF2 = f2.dodaj("osobaF2", "F2"); 

            o2+=osF2; //ne doda ga jer nisu iz istoga Fejsa
            o2.ispisiPrijatelje(); 

            o1-=o2;
            o1.ispisiPrijatelje(); 

            o1-=o2; //vec ga je obrisao, nista se ne desava
            o1.ispisiPrijatelje(); 

            f1.izbaci(o3);

            //Provjera medu prijatelja
            Fejs f3 = new Fejs(); 
            f2.imeFejsa = "f3";
            Osoba o31 = f3.dodaj("a", "aa");
            Osoba o32 = f3.dodaj("b", "bb"); 
            Osoba o33 = f3.dodaj("c", "cc");
            Osoba o34 = f3.dodaj("d", "dd");
            Osoba o35 = f3.dodaj("e", "ee"); 
            Osoba o36 = f3.dodaj("f", "ff");  
            Osoba o37 = f3.dodaj("g", "gg");  

            o31+=o32; 
            o31+=o33; 
            o31+=o35; 
            o31+=o36;
            o31+=o37;
            o32+=o33;
            o32+=o35;
            o33+=o34;
            o34+=o36;

            o31-=o35; 
            o31+=o34; 

            List<Osoba> li = o31.meduPrijatelji(o34);  
            Console.Write("\n MEDUPRIJATELJI");
            foreach( var prija in li){
                Console.Write(prija.Ime + " " + prija.Prezime + ", "); 
            } 

            //Provjera indeksiranja
            Fejs f4 = new Fejs(); 
            f2.imeFejsa = "f4";
            Osoba o41 = f4.dodaj("a", "aa");
            Osoba o42 = f4.dodaj("b", "aa"); 
            Osoba o43 = f4.dodaj("a", "aa");
            Osoba o44 = f4.dodaj("a", "aa");
            Osoba o45 = f4.dodaj("e", "ee"); 
            Osoba o46 = f4.dodaj("f", "aa");  
            Osoba o47 = f4.dodaj("g", "gg");  

            OsobeIstogPrezimena istoPrezime = f4["aa"];
            Console.Write("\n Prezime: " + "aa -->"); 
            foreach(var kori in istoPrezime.popisPrezime){
                Console.Write(kori.Ime + ", "); 
            }

            List<Osoba> istoPrezimeIme = istoPrezime["a"]; 
            Console.Write("\n Prezime pa Ime: " + "aa, a -->"); 
            foreach(var kori in istoPrezimeIme){
                Console.Write(kori.Ime + ", "); 
            }

            //PROVJERA SORTA
            Fejs f5 = new Fejs(); 
            f2.imeFejsa = "f5";
            Osoba o51 = f5.dodaj("a", "aa");
            Osoba o52 = f5.dodaj("b", "aa"); 
            Osoba o53 = f5.dodaj("c", "aa");
            Osoba o54 = f5.dodaj("d", "bb");
            Osoba o55 = f5.dodaj("e", "aa"); 
            Osoba o56 = f5.dodaj("f", "bb");  
            Osoba o57 = f5.dodaj("g", "gg");  

            o51+=o52; 
            o51+=o53; 
            o51+=o55; 
            o51+=o56;
            o51+=o57;
            o52+=o53;
            o52+=o55;
            o53+=o54;
            o54+=o56;

            f5.Sort();  //f5.SortMoj(); 
            f5.ispisKorisnika(); 

            //PROJERA FOREACH
            Console.Write("FOREACH--> ");
            foreach(var x in f5){
                Console.Write(x.Prezime + " " + x.Ime + ", "); 
            }

            //PROVJERA IZBACENIH IZ MREZE
            Fejs f6 = new Fejs(); 
            f2.imeFejsa = "f6";
            Osoba o61 = f6.dodaj("a", "aa");
            Osoba o62 = f6.dodaj("b", "bb"); 
            Osoba o63 = f6.dodaj("c", "cc");
            o61+=o62; 
            o61.ispisiPrijatelje(); 
            f6.izbaci(o63); 
            o61+=o63; 
            o61.ispisiPrijatelje(); 

            Console.ReadKey();
        }
    }
}
