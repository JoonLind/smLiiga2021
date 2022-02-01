using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;

namespace smLiiga2021
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        struct pelaajaTiedot
        {
            public string nro;
            public string nimi;
            public int maalienLkm;
        }
        pelaajaTiedot[] kotiPelaaja = new pelaajaTiedot[50];
        pelaajaTiedot[] vierasPelaaja = new pelaajaTiedot[50];
        int kotiMaalit = 0;
        int vierasMaalit = 0;
        string kotiJoukkue = "kotijoukkue";
        string vierasJoukkue = "vierasjoukkue";
        DateTime otteluPvm = DateTime.Now;
       



        public MainWindow()
        {
            InitializeComponent();
            XmlReader lukija = XmlReader.Create("smliiga.xml");
            string joukkue = "";
            lukija.MoveToContent();


            // lue niin kauan kuin Xml-tiedostossa riittää tietoa
            while (lukija.Read())
            {
                // luetaan joukkuetiedot
                if (lukija.NodeType == XmlNodeType.Element &&
                    lukija.Name == "Joukkue")

                {
                    if (lukija.HasAttributes)
                    {
                        joukkue = lukija.GetAttribute("nimi");
                        lstKotijoukkue.Items.Add(joukkue);
                        lstVierasjoukkue.Items.Add(joukkue);

                    }
          

                }

            }

            




        }

        void tyhjennäKotiPelaajaArray()
        {
            for (int i = 0; i < kotiPelaaja.Length; i++)
            {
                kotiPelaaja[i].nimi = "";
                kotiPelaaja[i].nro = "";
                kotiPelaaja[i].maalienLkm = 0;

            }

        }

        void tyhjennäVierasPelaajaArray()
        {
            for (int i = 0; i < vierasPelaaja.Length; i++)
            {
                vierasPelaaja[i].nimi = "";
                vierasPelaaja[i].nro = "";
                vierasPelaaja[i].maalienLkm = 0;
            }


        }




        private void lstKotijoukkue_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // ALIOHJELMA, JOKA TYHJENTÄÄ PELAAJIA SISÄLTÄVÄN ARRAY-TAULUKON
            tyhjennäKotiPelaajaArray();         //poistetaan muistista
            lstKotipelaajat.Items.Clear();      // poistetaan käyttöliittymästä
            kotiMaalit = 0;
            vierasMaalit = 0;
            string haettavaJoukkue = lstKotijoukkue.SelectedItem.ToString();
            kotiJoukkue = haettavaJoukkue;
            KirjaaPelinTiedot();
            lblKotijoukkue.Content = haettavaJoukkue;
            lblKotiMaalit.Content = kotiMaalit;
            
           



            XmlReader lukija = XmlReader.Create("smliiga.xml");
            string joukkue = "";
            lukija.MoveToContent();
            // tyhjätään listalaatikko
            // lstKotipelaajat.Items.Clear();

            
            // lue niin kauan kuin Xml-tiedostossa riittää tietoa
            int ind = 0;
            while (lukija.Read())
            {
                // luetaan joukkuetiedot
                if (lukija.NodeType == XmlNodeType.Element &&
                    lukija.Name == "Joukkue")

                {
                    if (lukija.HasAttributes)
                    {
                        joukkue = lukija.GetAttribute("nimi");
                        if (joukkue == haettavaJoukkue)
                        {
                            // luetaan yhden joukkueen pelaajat array-tauluun

                            while (lukija.Read())
                            {
                                // lopetetaan tämä silmukka, kun joukkue vaihtuu (huom. EndElement)
                                // break-käsky lopettaa silmukan
                                if (lukija.NodeType == XmlNodeType.EndElement &&
                                    lukija.Name == "Joukkue")
                                {
                                    break;
                                }
                                if (lukija.NodeType == XmlNodeType.Element &&
                                lukija.Name == "Nimi")
                                {
                                    lukija.Read();
                                    kotiPelaaja[ind].nimi = lukija.Value;
                                    lstKotipelaajat.Items.Add(lukija.Value);
                                }
                                if (lukija.NodeType == XmlNodeType.Element &&
                                     lukija.Name == "Pelaajanro")
                                {
                                    lukija.Read();
                                    kotiPelaaja[ind].nro = lukija.Value;
                                    ind++;
                                }




                            }
                        }
                    }





                }

            }



        }

        private void lstVierasjoukkue_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

            // ALIOHJELMA, JOKA TYHJENTÄÄ PELAAJIA SISÄLTÄVÄN ARRAY-TAULUKON
            tyhjennäVierasPelaajaArray();         //poistetaan muistista
            lstVieraspelaajat.Items.Clear();      // poistetaan käyttöliittymästä
            vierasMaalit = 0;
            

            string haettavaJoukkue = lstVierasjoukkue.SelectedItem.ToString();
            vierasJoukkue = haettavaJoukkue;
            KirjaaPelinTiedot();
            lblVierasjoukkue.Content = haettavaJoukkue;
            lblVierasMaalit.Content = vierasMaalit;


            XmlReader lukija = XmlReader.Create("smliiga.xml");
            string joukkue = "";
            lukija.MoveToContent();
            // tyhjätään listalaatikko
            // lstVieraspelaajat.Items.Clear();


            // lue niin kauan kuin Xml-tiedostossa riittää tietoa
            int ind = 0;
            while (lukija.Read())
            {
                // luetaan joukkuetiedot
                if (lukija.NodeType == XmlNodeType.Element &&
                    lukija.Name == "Joukkue")

                {
                    if (lukija.HasAttributes)
                    {
                        joukkue = lukija.GetAttribute("nimi");
                        if (joukkue == haettavaJoukkue)
                        {
                            // luetaan yhden joukkueen pelaajat array-tauluun

                            while (lukija.Read())
                            {
                                // lopetetaan tämä silmukka, kun joukkue vaihtuu (huom. EndElement)
                                // break-käsky lopettaa silmukan
                                if (lukija.NodeType == XmlNodeType.EndElement &&
                                    lukija.Name == "Joukkue")
                                {
                                    break;
                                }






                                if (lukija.NodeType == XmlNodeType.Element &&
                                lukija.Name == "Nimi")
                                {
                                    lukija.Read();
                                    vierasPelaaja[ind].nimi = lukija.Value;
                                    lstVieraspelaajat.Items.Add(lukija.Value);
                                }
                                if (lukija.NodeType == XmlNodeType.Element &&
                                     lukija.Name == "Pelaajanro")
                                {
                                    lukija.Read();
                                    vierasPelaaja[ind].nro = lukija.Value;
                                    ind++;
                                }


                            }
                        }
                    }
                }

            }
            
        }

      


    private void btnKirjaaKotiMaali_Click(object sender, RoutedEventArgs e)
        {
            if (lstKotipelaajat.SelectedIndex < 0)
            {
                MessageBox.Show("Valitse maalin tehnyt pelaaja");
                return;
            }
            // lisää maalin kellonaika
            DateTime tämäHetki = DateTime.Now;
            string kellonAika = tämäHetki.ToShortTimeString();

            int pelinro = lstKotipelaajat.SelectedIndex;
            lstKotiMaalit.Items.Add(kellonAika + " " + lstKotipelaajat.SelectedItem.ToString() + " " + kotiPelaaja[pelinro].nro);
            kotiMaalit++;
            lblKotiMaalit.Content = kotiMaalit;
           





            int maalinTekijänInd = lstKotipelaajat.SelectedIndex;
            kotiPelaaja[maalinTekijänInd].maalienLkm++;



            KirjaaPelinTiedot();
            Onnittele();
          



            // Aliohjelma käynnistetään vain, jos pelaaja on tehnyt vähintään 3 maalia.
            // Välitä aliohjelmalle "Onnittele" tiedot
            // 1. siitä, montako maalia pelaaja on tehnyt
            // 2. pelaajan nimi
            // Tee aliohjelma Onnittele, joka saa yllä mainitut tiedot
            // aliohjelman tehtävänä on onnitella hattutempusta tai
            // jos maaleja on yli 3 niin ilmoittaa maalien lukumäärä ja onnittelut.

        }



        void Onnittele()
        {
            int maalintekijänTiedot = lstKotipelaajat.SelectedIndex;
            string maalintekijä = kotiPelaaja[maalintekijänTiedot].nimi;
            int maalintekijäInd = lstKotipelaajat.SelectedIndex;
            int maali = kotiPelaaja[maalintekijäInd].maalienLkm;

            if (maali == 3)
            {
                MessageBox.Show("Onnittelut hattutempusta, " + maalintekijä + "!");
            }

            if (maali > 3)
            {
                MessageBox.Show("Onnittelut " + maalintekijä + "! Teit "+ maali + " maalia.");

            }

            return;

        }






            private void btnKirjaaVierasMaali_Click(object sender, RoutedEventArgs e)
        {
            if (lstVieraspelaajat.SelectedIndex < 0)
            {
                MessageBox.Show("Valitse maalin tehnyt pelaaja");
                return;
            }

            // lisää maalin kellonaika
            DateTime tämäHetki = DateTime.Now;
            string kellonAika = tämäHetki.ToShortTimeString();

            int pelinro = lstVieraspelaajat.SelectedIndex;
            lstVierasMaalit.Items.Add(kellonAika + " " + lstVieraspelaajat.SelectedItem.ToString() + " " + vierasPelaaja[pelinro].nro);
            vierasMaalit++;
            lblVierasMaalit.Content = vierasMaalit;

            int maalinTekijänInd = lstVieraspelaajat.SelectedIndex;
            vierasPelaaja[maalinTekijänInd].maalienLkm++;



            KirjaaPelinTiedot();
            OnnitteleVieraspelaajaa();


        }

        void OnnitteleVieraspelaajaa()
        {
            int maalintekijänTiedot = lstVieraspelaajat.SelectedIndex;
            string maalintekijä = vierasPelaaja[maalintekijänTiedot].nimi;
            int maalintekijäInd = lstVieraspelaajat.SelectedIndex;
            int maali = vierasPelaaja[maalintekijäInd].maalienLkm;

            if (maali == 3)
            {
                MessageBox.Show("Onnittelut hattutempusta, " + maalintekijä + "!");
            }

            if (maali > 3)
            {
                MessageBox.Show("Onnittelut " + maalintekijä + "! Teit " + maali + " maalia.");

            }

            return;

        }






        void KirjaaPelinTiedot()
        {
            // kirjaa joukkueiden nimet ja maalitilanteen muuttujien 
            // vierasjoukkue, kotijoukkue, kotiMaalit, vierasMaalit
            // ja otteluPvm perusteella

            
            
            lblPelintiedot.Content = otteluPvm.ToShortDateString()
                + " "
                + kotiJoukkue
                + " - "
                + vierasJoukkue;

            lblTilanne.Content = kotiMaalit + " - " + vierasMaalit;

            if (kotiJoukkue == vierasJoukkue)
            {
                TarkistaJoukkuevalinta();

            }




        }

        void TarkistaJoukkuevalinta()


        //{
        //  if (vierasJoukkue = kotiJoukkue)
        {
        MessageBox.Show("Tarkista joukkuevalinta");
        return;
        }
        //}


        // lisää tämä tms. oikeaan paikkaan 
        // if (kotiJoukkue = vierasJoukkue)
        //   {
        //     TarkistaJoukkuevalinta();
        //}



        private void clrottelunpvm_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {


            otteluPvm = (DateTime)clrottelunpvm.SelectedDate;

            KirjaaPelinTiedot();
            


        }

        private void btnNextTab_Click(object sender, RoutedEventArgs e)
        {
            
            int newIndex = välilehti.SelectedIndex + 1;
            välilehti.SelectedIndex = newIndex;
        }





    }
    }   
    


